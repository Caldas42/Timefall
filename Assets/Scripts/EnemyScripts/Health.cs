using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 200;
    [SerializeField] private int currencyWorth = 10;

    private float burnTimeRemaining = 0f;
    private float burnDPS = 0f;
    private bool isDead = false;

    public bool IsBurning => burnTimeRemaining > 0f;

    private void Update()
    {
        if (isDead) return;

        if (burnTimeRemaining > 0f)
        {
            TakeDamage(burnDPS * Time.deltaTime);
            burnTimeRemaining -= Time.deltaTime;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (isDead) return;

        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            isDead = true;
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
        }
    }

    public void ApplyBurn(float dps, float duration)
    {
        if (burnTimeRemaining <= 0f)
        {
            burnDPS = dps;
            burnTimeRemaining = duration;
        }
    }
}


