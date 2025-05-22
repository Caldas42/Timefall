using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }
}
