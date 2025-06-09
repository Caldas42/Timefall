using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectedManager : MonoBehaviour
{
    private bool ButtonState = true;

    [SerializeField] private AudioSource Music;

    [SerializeField] private Slider volumeSliderMusic;
    [SerializeField] private Slider volumeSliderSoundEffects;

    [SerializeField] private Sprite SoundOn;
    [SerializeField] private Sprite SoundOff;

    //[SerializeField] private Image muteImage;

    void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            load();
        }
        else
        {
            load();
        }
    }

    public void LigarDesligarSom()
    {
        ButtonState = !ButtonState;
        Music.enabled = ButtonState;

        if (ButtonState)
        {
            //muteImage.sprite = SoundOn;
        }
        else
        {
            //muteImage.sprite = SoundOff;  
        }
    }

    public void ChangeVolumeMusic()
    {
        AudioListener.volume = volumeSliderMusic.value;
    }

    /*
    public void ChanceVolumeSoundEffects()
    {
        AudioListener.volume = volumeSliderSoundEffects.value;
    }
    */

    public void load()
    {
        volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        volumeSliderSoundEffects.value = PlayerPrefs.GetFloat("SoundEffects");
    }

    public void save()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSliderMusic.value);
        PlayerPrefs.SetFloat("SoundEffects", volumeSliderSoundEffects.value);
    }
}
