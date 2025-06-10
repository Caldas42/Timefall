using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI remainingLivesUI;

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
    }
}
