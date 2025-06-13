using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicAndSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource Music;
    [SerializeField] private Slider volumeSliderMusic;
    [SerializeField] private TMP_Text musicVolumePercentage;
    [SerializeField] private Image musicToggleButtonImage;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    private bool ButtonState = true;
    private float lastVolume;

    void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }

        volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        AudioListener.volume = volumeSliderMusic.value;
        musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
    }

    void Update()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSliderMusic.value);
    }

    public void MusicOnOff()
    {
        ButtonState = !ButtonState;

        if (ButtonState)
        {
            PlayerPrefs.SetFloat("MusicVolume", lastVolume);
            volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
            musicToggleButtonImage.sprite = musicOnSprite;
            AudioListener.volume = volumeSliderMusic.value;
            musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
        }
        else
        {
            lastVolume = PlayerPrefs.GetFloat("MusicVolume");
            volumeSliderMusic.value = 0;
            AudioListener.volume = 0;
            musicToggleButtonImage.sprite = musicOffSprite;
            musicVolumePercentage.text = "0%";
        }
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = volumeSliderMusic.value;
        musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
        PlayerPrefs.SetFloat("MusicVolume", volumeSliderMusic.value);
    }
    
}