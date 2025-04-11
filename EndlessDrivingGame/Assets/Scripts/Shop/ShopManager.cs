using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopButton[] buttons;
    [SerializeField] private MoneyManager moneyManager;
    

    void Start()
    {
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
}
