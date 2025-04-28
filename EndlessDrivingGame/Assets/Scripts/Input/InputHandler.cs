using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{

    [SerializeField] private CarHandler carHandler;
    [SerializeField] private GameObject controller;

    private float accelerationInput = 0f;
    private float steeringInput = 0f;


    private void Awake() {
        if (!gameObject.CompareTag("Player")) {
            Destroy(controller);
            Destroy(this);
        }
    }

    private void Update()
   {

        Vector2 input = Vector2.zero;
      
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (carHandler != null)           
            carHandler.SetInput(input);
        //for phones
        input = new Vector2(steeringInput, accelerationInput);
        if(carHandler != null){
            carHandler.SetInput(input);
        }
            
    }
    // Эти функции будут вызываться EventTrigger на кнопках
    public void SetAccelerationInput(float input) {
        accelerationInput = input;
    }

    public void SetSteeringInput(float input) {
        steeringInput = input;
    }
}
