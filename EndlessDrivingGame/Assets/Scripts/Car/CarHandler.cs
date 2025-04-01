using System.Collections;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform gameModel;

    [SerializeField] private ExplodeHandler explodeHandler;
    
    float accelerationmultiplier = 3f;
    float brakeMultiplier = 15f;
    float steeringMultiplier = 5f;

    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;
    
    Vector2 input = Vector2.zero;

    bool isExploded = false;
    bool isPlayer;

    private void Start() {
        isPlayer = CompareTag("Player");
    }
    private void Update()
    {
        if(isExploded)
            return;
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
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
            if(collision.transform.root.CompareTag("Untagged"))
                return;

            if(collision.transform.root.CompareTag("CarAI"))
                return;

            Time.timeScale = 1.0f;
        }
        
        Vector3 velocity = rb.linearVelocity;
        explodeHandler.Explode(velocity * 45);

        isExploded = true;
        
        StartCoroutine(SlowDownTimeCO());
    }
}
