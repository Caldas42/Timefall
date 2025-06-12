using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;
    [SerializeField] private int currency = 80;
    [SerializeField] private int remainingLives = 20;
    [SerializeField] private LevelController levelController;

    public int getCurrency()
    {
        return currency;
    }

    public int getRemainingLives()
    {
        return remainingLives;
    }

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

        return false;
    }

    public void DamagePlayer(int amount)
    {
        remainingLives -= amount;

        if (remainingLives <= 0)
        {
            levelController.OpenGameOverPanel();
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
        }
    }

    public void Victory()
    {
        levelController.OpenWinPanel();
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
    
    public int GetRemainingLives()
    {
        return remainingLives;
    }
}