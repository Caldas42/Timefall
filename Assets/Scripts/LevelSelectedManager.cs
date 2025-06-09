using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectedManager : MonoBehaviour
{
    private bool ButtonState = true;

    [SerializeField]private AudioSource Music;

    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Sprite SoundOn;
    [SerializeField] private Sprite SoundOff;

    //[SerializeField] private Image muteImage;

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
}
