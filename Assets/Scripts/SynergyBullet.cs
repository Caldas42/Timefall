using UnityEngine;

public class SynergyBullet : Bullet
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private float synergyBurnDuration = 4f;
    [SerializeField] private float synergyBurnDamagePerSecond = 5f;
    [SerializeField] private GameObject fireEffectPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask);
        Debug.Log("Inimigos detectados: " + hits.Length);

        for (int i = 0; i < hits.Length; i++)
        {
            Health enemyHealth = hits[i].GetComponent<Health>();
            if (enemyHealth != null)
            {
                // Aplica dano imediato (como AreaBullet)
                enemyHealth.TakeDamage(getBulletDamage());

                // Aplica efeito burn (como BurnBullet)
                enemyHealth.ApplyBurn(synergyBurnDamagePerSecond, synergyBurnDuration);

                // Instancia efeito visual de fogo (se existir)
                if (fireEffectPrefab != null)
                {
                    GameObject fireFX = Instantiate(fireEffectPrefab, enemyHealth.transform);
                    fireFX.transform.localPosition = Vector3.zero;
                    Destroy(fireFX, synergyBurnDuration);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
