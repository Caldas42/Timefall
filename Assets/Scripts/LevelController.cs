using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI remainingLivesText;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI speedText;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    private float gameSpeed = 1f;

    public float GetGameSpeed()
    {
        return PlayerPrefs.GetFloat("GameSpeed", 1f);
    }

    void OnGUI()
    {
        currencyText.text = LevelManager.main.getCurrency().ToString();
        remainingLivesText.text = LevelManager.main.getRemainingLives().ToString();
    }

    public void ToggleSpeed()
    {
        if (gameSpeed == 1f)
        {
            gameSpeed = 2f;
        }
        else
        {
            gameSpeed = 1f;
        }

        Time.timeScale = gameSpeed;

        PlayerPrefs.SetFloat("GameSpeed", Time.timeScale);
        PlayerPrefs.Save();
        speedText.text = Time.timeScale == 1f ? "2X" : "1X";
    }

    public void CallScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = PlayerPrefs.GetFloat("GameSpeed", 1f);
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
}
