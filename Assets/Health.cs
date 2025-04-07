using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2;

    public void TakeDamage(float dmg){
        hitPoints -= dmg;

        if (hitPoints <= 0){
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
