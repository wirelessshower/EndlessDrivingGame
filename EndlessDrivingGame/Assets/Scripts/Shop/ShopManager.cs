using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopButton[] buttons;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private Button backButton;
    
    

    void Start()
    {
        backButton.onClick.AddListener(Back);

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
    }
}
