using UnityEngine;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class ControlSettings : MonoBehaviour {
    public static ControlSettings Instance { get; private set; }
    public static Devise currentDevise = Devise.PC; // Установка управления по умолчанию

    [SerializeField] private Button DevisePC;
    [SerializeField] private Button DevisePhone;

    void Awake() {
        if (Instance == null) {
            Instance = this;            
        } else {
            Destroy(gameObject); // Уничтожаем дубликаты
        }
    }

    void Start() {
        bool isTuchDevice = YG2.envir.isMobile;

        
        // При запуске игры применяем сохраненные настройки управления (если есть)
        if (isTuchDevice)
            SetDevisePhone();
        else
            SetDevisePC();


        LoadControlSettings();
        DevisePC.onClick.AddListener(SetDevisePC);
        DevisePhone.onClick.AddListener(SetDevisePhone);
    } 

    // Функция для загрузки сохраненных настроек управления
    void LoadControlSettings() {
        if (PlayerPrefs.HasKey("ControlType")) {
            currentDevise = (Devise)PlayerPrefs.GetInt("ControlType");
        }
    }

    public void SetDevisePC() {
        currentDevise = Devise.PC;

        Image PCButtonImage = DevisePC.GetComponent<Image>();
        Image PhoneButtonImage = DevisePhone.GetComponent<Image>();

        float red = HexToFloat("F5");
        float green = HexToFloat("DD");
        float blue = HexToFloat("7E");

        float red1 = HexToFloat("EA");
        float green1 = HexToFloat("BB");
        float blue1 = HexToFloat("00");

        PhoneButtonImage.color = new Color(red, green, blue);
        PCButtonImage.color = new Color(red1, green1, blue1);
        PlayerPrefs.SetInt("ControlType", (int)currentDevise);
    }
    public void SetDevisePhone() {
        currentDevise = Devise.Phone;

        Image PCButtonImage = DevisePC.GetComponent<Image>();
        Image PhoneButtonImage = DevisePhone.GetComponent<Image>();

        float red = HexToFloat("F5");
        float green = HexToFloat("DD");
        float blue = HexToFloat("7E");

        float red1 = HexToFloat("EA");
        float green1 = HexToFloat("BB");
        float blue1 = HexToFloat("00");

        PCButtonImage.color = new Color(red, green, blue);
        PhoneButtonImage.color = new Color(red1, green1, blue1);

        PlayerPrefs.SetInt("ControlType", (int)currentDevise);
    }

    private float HexToFloat(string hex) {
        int decimalValue = System.Convert.ToInt32(hex, 16);
        return (float)decimalValue / 255f;
    }

}

//F5DD7E EABB00

public enum Devise {
    PC,
    Phone
}