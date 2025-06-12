using UnityEngine;

public class BasicBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(GetBulletDamage());
        Destroy(gameObject);
    }
}