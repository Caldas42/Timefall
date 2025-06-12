using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private float bulletDamage = 25;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    protected float GetBulletDamage()
    {
        return bulletDamage;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;

        // Faz a rotação da bala para apontar na direção
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle; // isso rotaciona o corpo 2D
    }
}
