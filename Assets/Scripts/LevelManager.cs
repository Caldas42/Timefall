using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
   public static LevelManager main;
   public Transform startPoint;
   public Transform[] path;
   public TMP_Text livesText;

   public int currency; 
   public int remainingLives;
   [SerializeField] private UIManager uIManager;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
        remainingLives = 5;
    }

    public void IncreaseCurrency(int amount){
        currency+=amount;
    }

    public bool SpendCurrency(int amount){
        if(amount<=currency){
            currency -= amount;
            return true;
        }else{
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    public void DamagePlayer(int amount){

        remainingLives -= amount;

        if (remainingLives <= 0)
        {
            uIManager.OpenGameOverPanel();
        }

    }
}
