using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private float bulletDamage = 25;

    private Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    protected float getBulletDamage() {
        return bulletDamage;
    }

    private void FixedUpdate()
    {
        if(target) { 
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}
