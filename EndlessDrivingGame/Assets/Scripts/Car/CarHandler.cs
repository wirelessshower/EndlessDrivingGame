using System;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform gameModel;
    
    float accelerationmultiplier = 3f;
    float brakeMultiplier = 15f;
    float steeringMultiplier = 5f;

    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;
    
    Vector2 input = Vector2.zero;

    private void Update()
    {
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
    }

    private void FixedUpdate()
    {
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
}
