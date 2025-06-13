using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject MapPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject ExitPanel;

    public void CallScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenMapPanel()
    {
        MapPanel.SetActive(true);

        SettingsPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        SettingsPanel.SetActive(true);

        MapPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }
    public void OpenExitPanel()
    {
        ExitPanel.SetActive(true);
    }

    public void CloseExitPanel()
    {
        ExitPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
