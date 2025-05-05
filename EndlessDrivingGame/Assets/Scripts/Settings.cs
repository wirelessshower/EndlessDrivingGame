using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class Settings : MonoBehaviour {
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    [Header("Close")]
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button CloseOutBorder;

    private void Start() {
        if (PlayerPrefs.HasKey("MusicVolume"))
            LoadVolume();
        else
            SetAudioVolume();

        CloseSettings();
        CloseButton.onClick.AddListener(CloseSettings);
        CloseOutBorder.onClick.AddListener(CloseSettings);
    }

    public void SetAudioVolume() {
        float value = slider.value;
        audioMixer.SetFloat("Audio", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    
        
    private void LoadVolume() {
        slider.value = PlayerPrefs.GetFloat("MusicVolume");

        SetAudioVolume();
    }

    private void CloseSettings() { 
        gameObject.SetActive (false);
        CloseOutBorder.gameObject.SetActive (false);
        PlayerPrefs.Save();
    }

}
