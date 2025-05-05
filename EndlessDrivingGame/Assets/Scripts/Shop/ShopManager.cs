using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

using PlayerPrefs = RedefineYG.PlayerPrefs;
public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopButton[] buttons;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private Button backButton;
    [SerializeField] private Button rewardedButton;

    public string rewardID;



    void Start()
    {
        backButton.onClick.AddListener(Back);
        rewardedButton.onClick.AddListener(PlusFiveHundred);

        buttons = GetComponentsInChildren<ShopButton>();

        for(int i= 0; i < buttons.Length; i++){            
            buttons[i].SetManager(this);
        }
        UpdateInfoButton();    
    }

    public void UpdateInfoButton(){
        
        for(int i = 0; i < buttons.Length; i++){
            buttons[i].UpdateInfo();
        }
        
        moneyManager.UpdateMoney();
    }

    void Back()
    {
        SceneManager.LoadScene("Manu");
        PlayerPrefs.Save();
    }

    void PlusFiveHundred() {
        YG2.RewardedAdvShow(rewardID, () => {
            moneyManager.AddMoney(500);
             rewardedButton.interactable = false;
            // Запускаем корутину для включения кнопки через 60 секунд
            StartCoroutine(EnableRewardButtonAfterDelay(60f));
        });
    }

     IEnumerator EnableRewardButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        rewardedButton.interactable = true; // Снова делаем кнопку активной
    }
}

    

