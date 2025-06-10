using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject image;
    [SerializeField] private GameObject button;

    public void OpenSettings()
    {
        //SceneManager.LoadScene("Settings");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenImage()
    {
        image.SetActive(true);
        button.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
