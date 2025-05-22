using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
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
}
