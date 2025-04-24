using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManuManager : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button store;
    [SerializeField] Button settings;
    [SerializeField] Image carImage;
    [SerializeField] Sprite[] sprites;



    void Start()
    {

        int index = PlayerPrefs.GetInt("CurrentCar");
        carImage.sprite = sprites[index];
        

        play.onClick.AddListener(OpenGame);
        store.onClick.AddListener(OpenStore);        
    }

    private void OpenStore()
    {
        SceneManager.LoadScene("Store");
    }

    private void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    private void OpenGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
