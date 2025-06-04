using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI remainingLivesUI;

    void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        remainingLivesUI.text = LevelManager.main.remainingLives.ToString();
    }

}
