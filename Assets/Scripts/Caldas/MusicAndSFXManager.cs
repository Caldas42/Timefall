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

            if (volumeSliderMusic != null)
            {
                volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
        else
        {
            if (volumeSliderMusic != null)
            {
                volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    public void MusicOnOff()
    {
        ButtonState = !ButtonState;

        if (ButtonState)
        {
            AudioListener.volume = volumeSliderMusic.value;
            musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
        }
        else
        {
            AudioListener.volume = 0;
            musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 0) + "%";
        }
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = volumeSliderMusic.value;
        musicVolumePercentage.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
    }
    
}
