using UnityEngine;

public class AreaBullet : Bullet
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask enemyMask;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask);

        int i = 0;

        while (i < hits.Length) {
            hits[i].gameObject.GetComponent<Health>().TakeDamage(GetBulletDamage());
            i++;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}