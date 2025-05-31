using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    public int currency = 100;
    public int remainingLives = 20;
    [SerializeField] private UIManager uIManager;

    private void Awake()
    {
        main = this;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    public void DamagePlayer(int amount)
    {

        remainingLives -= amount;

        if (remainingLives <= 0)
        {
            uIManager.OpenGameOverPanel();
        }

    }
    private bool gameEnded = false;

    public void CheckVictoryCondition(int currentWave, int maxWaves, int enemiesRemaining, bool isSpawning)
    {
        if (gameEnded) return;

        // Se todas as waves passaram, não há mais inimigos vivos nem a spawnar, e não está mais spawnando
        if (currentWave >= maxWaves && enemiesRemaining <= 0 && !isSpawning)
        {
            gameEnded = true;
            uIManager.OpenWinPanel();
        }
    }

}
