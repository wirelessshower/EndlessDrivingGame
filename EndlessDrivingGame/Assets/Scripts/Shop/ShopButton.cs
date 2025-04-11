using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public int carPrice = 100;
    public Button buyButton;
    public TMP_Text statusText;
    public TMP_Text price;
    public int CarIndex;
    public Sprite cars;
    public Image image;
    public GameObject Lock;

    private ShopManager manager;    

    void Start()
    {
        buyButton.onClick.AddListener(BuyCar);

        PlayerPrefs.SetInt("CarBought_" + 0, 1);
       
        image.sprite = cars;
        UpdateInfo();
        
    }

    public void SetManager(ShopManager shopManager){
       manager = shopManager;
    }

   

    public void UpdateInfo(){
        int currentCarIndex = PlayerPrefs.GetInt("CurrentCar");

        if(currentCarIndex == CarIndex){
            statusText.text = "Выбрана";
            price.text = " ";
            Lock.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("CarBought_" + CarIndex, 0) == 1){
            price.text = " ";
            statusText.text = "Куплена";
            Lock.SetActive(false);
        }
        else{
            Lock.SetActive(true);
            price.text = carPrice.ToString();
        }
        
    }

    void BuyCar()
    {
        int money = PlayerPrefs.GetInt("Money", 10000);
        if(money >= carPrice){
            if(PlayerPrefs.GetInt("CarBought_" + CarIndex, 0) != 1){
                money -= carPrice;
                PlayerPrefs.SetInt("Money", money);
                PlayerPrefs.SetInt("CarBought_" + CarIndex, 1);

                PlayerPrefs.SetInt("CurrentCar", CarIndex);

                manager.UpdateInfoButton();                
            } else{
            PlayerPrefs.SetInt("CurrentCar", CarIndex);
            manager.UpdateInfoButton();
            }
        }else{
            Debug.Log("Не хватает денег");
        }
       
        
    }

}
