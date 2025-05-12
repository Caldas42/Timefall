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
            StartCoroutine(ApplyBurn(enemyHealth));
        }

        Destroy(gameObject);
    }

    private System.Collections.IEnumerator ApplyBurn(Health target)
    {
        GameObject fireFX = null;

        if (fireEffectPrefab != null)
        {
            fireFX = Instantiate(fireEffectPrefab, target.transform);
            fireFX.transform.localPosition = Vector3.zero;
        }

        float elapsed = 0f;
        while (elapsed < burnDuration)
        {
            target.TakeDamage(burnDamagePerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (fireFX != null)
        {
            Destroy(fireFX);
        }
    }
}

