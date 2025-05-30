using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject wallpaper;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject skipButton;


    public void StartGame()
    {
        wallpaper.SetActive(false);
        playButton.SetActive(false);
        skipButton.SetActive(true);
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    public void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
