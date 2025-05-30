using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f; // Bullets Per Second

    private Transform target;
    private float timeUntilFire;

    // --- Sinergia ---
    private GameObject synergyBulletPrefab;
    private bool synergyActive = false;

    private bool isSinergia = false;  // ✅ NOVA VARIÁVEL

    private bool canShoot = true;

    private void Update()
    {
        if (!canShoot) return;

        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void Shoot()
    {
        GameObject prefabToUse = synergyActive ? synergyBulletPrefab : bulletPrefab;

        GameObject bulletObj = Instantiate(prefabToUse, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    public void SetSynergyBullet(GameObject synergyBullet)
    {
        synergyBulletPrefab = synergyBullet;
        synergyActive = true;
        isSinergia = true;  // ✅ MARCA QUE JÁ FEZ SINERGIA
    }

    public void ResetSynergy()
    {
        synergyActive = false;
        synergyBulletPrefab = null;
        isSinergia = false;  // ✅ PERMITE SINERGIA NOVAMENTE
    }

    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }

    public bool HasSinergia()
    {
        return isSinergia;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
