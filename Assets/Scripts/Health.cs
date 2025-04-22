using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    public void TakeDamage(float dmg){
        hitPoints -= dmg;

        if (hitPoints <= 0){
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
        }
    }
}
