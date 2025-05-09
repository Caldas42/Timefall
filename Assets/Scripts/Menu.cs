using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI remainingLivesUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        remainingLivesUI.text = "Lives remaining: " + LevelManager.main.remainingLives.ToString();
    }

    public void SetSelected(){

    }
}
