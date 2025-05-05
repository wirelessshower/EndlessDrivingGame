using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
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
        if(YG2.isTimerAdvCompleted)
            YG2.InterstitialAdvShow();
        SceneManager.LoadScene("Manu");
    }

    private void ResetartScene()
    {
        if(YG2.isTimerAdvCompleted)
            YG2.InterstitialAdvShow();
        SceneManager.LoadScene("MainLevel");
    }

    private void OpenShop()
    {
        if(YG2.isTimerAdvCompleted)
            YG2.InterstitialAdvShow();
        SceneManager.LoadScene("Store");
    }

    private void OnCarCrash() {
        Debug.Log("Hide");
        gameObject.SetActive(true);
        animator.SetTrigger("Show");
    }
}
