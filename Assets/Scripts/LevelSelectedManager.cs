using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectedManager : MonoBehaviour
{
    private bool ButtonState = true;

    [SerializeField] private AudioSource Music;

    
    [SerializeField] private TMP_Text volumeMusicText;

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
            volumeMusicText.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
        }
        else
        {
            //muteImage.sprite = SoundOff;  
            volumeMusicText.text = Mathf.RoundToInt(volumeSliderMusic.value * 0) + "%";
        }
    }

    public void ChangeVolumeMusic()
    {
        AudioListener.volume = volumeSliderMusic.value;
        volumeMusicText.text = Mathf.RoundToInt(volumeSliderMusic.value * 100) + "%";
    }

    /*
    public void ChanceVolumeSoundEffects()
    {
        AudioListener.volume = volumeSliderSoundEffects.value;
    }
    */

    public void load()
{
    if (volumeSliderMusic != null)
    {
        volumeSliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    /*
    if (volumeSliderSoundEffects != null && PlayerPrefs.HasKey("SoundEffects"))
        {
            volumeSliderSoundEffects.value = PlayerPrefs.GetFloat("SoundEffects");
        }
    */
}


    public void save()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSliderMusic.value);
        //PlayerPrefs.SetFloat("SoundEffects", volumeSliderSoundEffects.value);
    }
}
