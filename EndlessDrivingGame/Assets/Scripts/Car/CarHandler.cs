using System;
using System.Collections;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform gameModel;

    [SerializeField] private ExplodeHandler explodeHandler;

    [Header("SFX")]
    [SerializeField] AudioSource carEngineAS;
    [SerializeField] AnimationCurve carPitchAnimationCurve;

    [SerializeField] AudioSource carSkidAS;
    [SerializeField] AudioSource carCrashAS;

    //Events
    public event Action CarCrash;

    
    float accelerationmultiplier = 3f;
    float brakeMultiplier = 15f;
    float steeringMultiplier = 5f;

    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;
    float carMaxSpePercentage = 0;
    
    Vector2 input = Vector2.zero;

    bool isExploded = false;
    bool isPlayer;

    
        

    private void Start() {
        isPlayer = CompareTag("Player");

        if(isPlayer)
            carEngineAS.Play();
    }
    private void Update()
    {
        if(isExploded){
            FadeOutCarAudio();
            return;
        }
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);

        UpdateCarAudio();
    }

    private void FixedUpdate()
    {
        if(isExploded)
        {
            rb.linearDamping = rb.linearVelocity.z * 0.1f;
            rb.linearDamping = Mathf.Clamp(rb.linearDamping, 1.5f, 10);

            rb.MovePosition(Vector3.Lerp(transform.position, new Vector3(0,0, transform.position.z), Time.deltaTime * .5f));

            return;
        }
        if (input.y > 0)
            Accelerate();
        else
            rb.linearDamping = 0.2f;
        
        if (input.y < 0)
            Brake();
        
        Steer();

        if (rb.linearVelocity.z <= 0)
            rb.linearVelocity = Vector3.zero;
    }
    
    void Accelerate()
    {
        rb.linearDamping = 0;
        
        if(rb.linearVelocity.z >= maxForwardVelocity)
            return;
        
        rb.AddForce(rb.transform.forward * accelerationmultiplier * input.y);
    }

    void Brake()
    {
        if(rb.linearVelocity.z <= 0)
            return;
        
        rb.AddForce(rb.transform.forward * brakeMultiplier * input.y);
    }

    void Steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            float speedBaseSteerLimit = rb.linearVelocity.z / 5f;
            speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteerLimit);
                        
            rb.AddForce(rb.transform.right * steeringMultiplier  * input.x * speedBaseSteerLimit);

            float normalizedX = rb.linearVelocity.x / maxSteerVelocity;
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);
            
            rb.linearVelocity = new Vector3(normalizedX * maxSteerVelocity, 0, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0,0, rb.linearVelocity.z), Time.fixedDeltaTime * 3);
        }
    }

    void UpdateCarAudio(){
        if(!isPlayer)
            return;

        carMaxSpePercentage = rb.linearVelocity.z / maxForwardVelocity;

        carEngineAS.pitch = carPitchAnimationCurve.Evaluate(carMaxSpePercentage);

        if(input.y < 0 && carMaxSpePercentage > 0.2f){
            if(!carSkidAS.isPlaying)
                carSkidAS.Play();
            
            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 1.0f, Time.deltaTime * 10);
        }else{
            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 0, Time.deltaTime * 30);
        }
    }

    void FadeOutCarAudio(){
        if(!isPlayer)
            return;

        carEngineAS.volume = Mathf.Lerp(carEngineAS.volume, 0, Time.deltaTime * 10);
        carEngineAS.volume = Mathf.Lerp(carSkidAS.volume, 0, Time.deltaTime * 10);
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        input = inputVector;
    }

    public void SetMaxSpeed(float newMaxSpeed){
        maxForwardVelocity = newMaxSpeed;
    }

    IEnumerator SlowDownTimeCO(){
        while(Time.timeScale > 0.2f){
            Time.timeScale -= Time.deltaTime *2;
            yield return null;
        }

        yield return new WaitForSeconds(.5f);

        while(Time.timeScale <= 1f){
            Time.timeScale += Time.deltaTime;

            yield return null;
        }

        Time.timeScale = 1.0f;
    }

    //events 
    void OnCollisionEnter(Collision collision)
    {
        if(!isPlayer){
            Time.timeScale = 1.0f;
            
            if(collision.transform.root.CompareTag("Untagged"))
                return;

            if(collision.transform.root.CompareTag("CarAI"))
                return;
            
            if(collision.transform.root.CompareTag("Coin"))
                return;

        }

        
        

        Vector3 velocity = rb.linearVelocity;
        explodeHandler.Explode(velocity * 45);

        isExploded = true;

        carCrashAS.volume = carMaxSpePercentage;
        carCrashAS.volume = Mathf.Clamp(carCrashAS.volume, 0.25f, 1.0f);

        carCrashAS.pitch = carMaxSpePercentage;
        carCrashAS.pitch = Mathf.Clamp(carCrashAS.pitch, 0.3f, 1.0f);

        carCrashAS.Play();
        CarCrash?.Invoke();
        
        StartCoroutine(SlowDownTimeCO());

    }

   
}
