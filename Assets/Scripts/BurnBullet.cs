using UnityEngine;

public class BurnBullet : Bullet
{
    [SerializeField] private float burnDuration = 3f;
    [SerializeField] private float burnDamagePerSecond = 1f;
    [SerializeField] private GameObject fireEffectPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health enemyHealth = other.gameObject.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.ApplyBurn(burnDamagePerSecond, burnDuration);

            if (fireEffectPrefab != null)
            {
                GameObject fireFX = Instantiate(fireEffectPrefab, enemyHealth.transform);
                fireFX.transform.localPosition = Vector3.zero;

                Destroy(fireFX, burnDuration);
            }
        }

        Destroy(gameObject);
    }
}

