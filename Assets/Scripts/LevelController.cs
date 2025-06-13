using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI remainingLivesText;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Image gameSpeedImage;
    [SerializeField] private Sprite gameSpeedOffSprite;
    [SerializeField] private Sprite gameSpeedOnSprite;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject BackwinPanel;
    [SerializeField] private GameObject BackLosePanel;


    private float gameSpeed = 1f;

    public float GetGameSpeed()
    {
        return PlayerPrefs.GetFloat("GameSpeed", 1f);
    }

void OnGUI()
{
    if (LevelManager.main != null)
    {
        if (currencyText != null)
            currencyText.text = LevelManager.main.getCurrency().ToString();

        if (remainingLivesText != null)
            remainingLivesText.text = LevelManager.main.getRemainingLives().ToString();
    }
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
        gameSpeedImage.sprite = Time.timeScale == 1f ? gameSpeedOnSprite : gameSpeedOffSprite;
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
        BackLosePanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseGameOverPanel()
    {
        gameOverPanel.SetActive(false);
        BackLosePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
        BackwinPanel.SetActive(false);
        Time.timeScale = 0f;
    }
    
    public void CloseWinPanel()
    {
        winPanel.SetActive(false);
        BackwinPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
