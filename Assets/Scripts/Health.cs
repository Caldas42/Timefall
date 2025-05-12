using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private float burnTimeRemaining = 0f;
    private float burnDPS = 0f;

    private void Update()
    {
        if (burnTimeRemaining > 0f)
        {
            TakeDamage(burnDPS * Time.deltaTime);
            burnTimeRemaining -= Time.deltaTime;
        }
    }

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
        }
    }

    public void ApplyBurn(float dps, float duration)
    {
        burnDPS = dps;
        burnTimeRemaining = duration;
    }
}

