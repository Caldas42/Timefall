using UnityEngine;
using UnityEngine.UI;

public class SynergyManager : MonoBehaviour
{
    [SerializeField] private GameObject synergyBulletPrefab;
    [SerializeField] private Button synergyButton;

    private Turret[] allTurrets;

    private void Start()
    {
        synergyButton.gameObject.SetActive(false);
        synergyButton.onClick.AddListener(ActivateSynergy);
    }

    private void Update()
    {
    allTurrets = Object.FindObjectsByType<Turret>(FindObjectsSortMode.None);

        bool synergyPossible = false;

        for (int i = 0; i < allTurrets.Length; i++)
        {
            for (int j = i + 1; j < allTurrets.Length; j++)
            {
                if (Vector2.Distance(allTurrets[i].transform.position, allTurrets[j].transform.position) < 1.5f)
                {
                    if (IsAreaTurret(allTurrets[i]) && IsFireTurret(allTurrets[j]) ||
                        IsFireTurret(allTurrets[i]) && IsAreaTurret(allTurrets[j]))
                    {
                        synergyPossible = true;
                        break;
                    }
                }
            }
            if (synergyPossible) break;
        }

        synergyButton.gameObject.SetActive(synergyPossible);

        if (!synergyPossible)
        {
            // Reseta tudo se sinergia não possível (botão some)
            foreach (var turret in allTurrets)
            {
                turret.ResetSynergy();
                turret.SetCanShoot(true);
            }
        }
    }

    private bool IsFireTurret(Turret turret)
    {
        return turret.name.ToLower().Contains("fire");
    }

    private bool IsAreaTurret(Turret turret)
    {
        return turret.name.ToLower().Contains("area");
    }

    private void ActivateSynergy()
    {
        foreach (var turret in allTurrets)
        {
            if (IsAreaTurret(turret))
            {
                turret.SetSynergyBullet(synergyBulletPrefab);
                turret.SetCanShoot(true);
            }
            else if (IsFireTurret(turret))
            {
                turret.ResetSynergy();
                turret.SetCanShoot(false);
            }
            else
            {
                turret.ResetSynergy();
                turret.SetCanShoot(true);
            }
        }
    }
}
