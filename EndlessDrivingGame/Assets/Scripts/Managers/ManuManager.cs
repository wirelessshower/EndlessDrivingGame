using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class ManuManager : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button store;
    [SerializeField] Button settings;
    [SerializeField] Image carImage;
    [SerializeField] GameObject settingsImage;
    [SerializeField] private Button CloseOutBorder;
    [SerializeField] Sprite[] sprites;



    void Start()
    {

        int index = PlayerPrefs.GetInt("CurrentCar");
        carImage.sprite = sprites[index];
        

        play.onClick.AddListener(OpenGame);
        store.onClick.AddListener(OpenStore); 
        settings.onClick.AddListener(OpenSettings);
    }

    private void OpenStore()
    {
        SceneManager.LoadScene("Store");
    }

    private void OpenSettings()
    {
        settingsImage.SetActive(true);
        CloseOutBorder.gameObject.SetActive(true);
    }

    private void OpenGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
