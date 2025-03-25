using System;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    float accelerationmultiplier = 3f;
    float brakeMultiplier = 15f;
    float steeringMultiplier = 5f;
    
    Vector2 input = Vector2.zero;

    private void FixedUpdate()
    {
        if (input.y > 0)
            Accelerate();
        else
            rb.linearDamping = 0.2f;
        
        if (input.x > 0)
            Brake();
        
        Steer();
    }
    
    void Accelerate()
    {
        rb.linearDamping = 0;
        
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
            rb.AddForce(rb.transform.right * steeringMultiplier  * input.x);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        input = inputVector;
    }
}
