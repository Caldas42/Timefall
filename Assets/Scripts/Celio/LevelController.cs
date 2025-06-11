using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI remainingLivesUI;
    [SerializeField] private TextMeshProUGUI speedText;

    private bool isFast = false;

    void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        remainingLivesUI.text = LevelManager.main.remainingLives.ToString();
    }

    public void ToggleSpeed()
    {
        if (isFast)
        {
            Time.timeScale = 1f;
            isFast = false;
        }
        else
        {
            Time.timeScale = 2f;
            isFast = true;
        }
        PlayerPrefs.SetFloat("GameSpeed", Time.timeScale);
        PlayerPrefs.Save();
        speedText.text = Time.timeScale == 1f ? "2X" : "1X";
    }
    
}
