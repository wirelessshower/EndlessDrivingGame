using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{

   [SerializeField] CarHandler carHandler;

    void Awake()
    {
        if(!CompareTag("Player")){
            Destroy(this);
            return;
        }
    }
    private void Update()
   {
      Vector2 input = Vector2.zero;
      
      input.x = Input.GetAxis("Horizontal");
      input.y = Input.GetAxis("Vertical");
      
      carHandler.SetInput(input);

      if (Input.GetKeyDown(KeyCode.R))
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         Time.timeScale = 1.0f;
      }
   }
}
