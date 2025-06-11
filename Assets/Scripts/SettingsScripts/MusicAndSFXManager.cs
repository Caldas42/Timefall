using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicAndSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource Music;
    [SerializeField] private Slider volumeSliderMusic;
    [SerializeField] private TMP_Text musicVolumePercentage;

    private bool ButtonState = true;

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

    public void MusicOnOff()
    {
        ButtonState = !ButtonState;

        if (ButtonState)
        {
            volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
            AudioListener.volume = volumeSliderMusic.value;
            musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
        }
        else
        {
            AudioListener.volume = 0;
            volumeSliderMusic.value = 0;
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
