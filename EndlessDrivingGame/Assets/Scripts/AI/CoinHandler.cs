using UnityEngine;

public class CoinHandler : MonoBehaviour
{  

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log(other.gameObject.CompareTag("Player"));
            int money = PlayerPrefs.GetInt("Money", 0);
            PlayerPrefs.SetInt("Money", money+ 10);
            gameObject.SetActive(false);
        }
    }   

}
