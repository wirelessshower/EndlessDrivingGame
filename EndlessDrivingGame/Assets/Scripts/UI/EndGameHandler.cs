using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] private Button home;
    [SerializeField] private Button restart;
    [SerializeField] private Button shop;

    private CarHandler carHandler;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
      // animator.SetTrigger("Hide");
        do { 
            carHandler = GameObject.FindWithTag("Player").GetComponent<CarHandler>();
        }while(carHandler == null);
        home.onClick.AddListener(OpenMainManue);
        restart.onClick.AddListener(ResetartScene);
        shop.onClick.AddListener(OpenShop);
        if (carHandler != null)
            carHandler.CarCrash += OnCarCrash;
        gameObject.SetActive(false);
    }    

    private void OpenMainManue(){
        SceneManager.LoadScene("Manu");
    }

    private void ResetartScene()
    {
        SceneManager.LoadScene("MainLevel");
    }

    private void OpenShop()
    {
        SceneManager.LoadScene("Store");
    }

    private void OnCarCrash() {
        Debug.Log("Hide");
        gameObject.SetActive(true);
        animator.SetTrigger("Show");
    }
}
