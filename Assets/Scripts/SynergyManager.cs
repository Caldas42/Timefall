using UnityEngine;
using UnityEngine.UI;

public class SynergyManager : MonoBehaviour
{
    [SerializeField] private GameObject synergyBulletPrefab;
    [SerializeField] private Button synergyButton;

    private Turret[] allTurrets;

    private Turret turretArea;  // ✅ Para armazenar o par encontrado
    private Turret turretFogo;

    private void Start()
    {
        synergyButton.gameObject.SetActive(false);
        synergyButton.onClick.AddListener(ActivateSynergy);
    }

    private void Update()
    {
        allTurrets = Object.FindObjectsByType<Turret>(FindObjectsSortMode.None);

        bool synergyPossible = false;

        // Reset do par encontrado
        turretArea = null;
        turretFogo = null;

        for (int i = 0; i < allTurrets.Length; i++)
        {
            for (int j = i + 1; j < allTurrets.Length; j++)
            {
                if (Vector2.Distance(allTurrets[i].transform.position, allTurrets[j].transform.position) < 1.5f)
                {
                    // Só faz sinergia se NENHUM dos dois tiver sinergia
                    if (!allTurrets[i].HasSinergia() && !allTurrets[j].HasSinergia())
                    {
                        if (IsAreaTurret(allTurrets[i]) && IsFireTurret(allTurrets[j]))
                        {
                            turretArea = allTurrets[i];
                            turretFogo = allTurrets[j];
                            synergyPossible = true;
                            break;
                        }
                        else if (IsFireTurret(allTurrets[i]) && IsAreaTurret(allTurrets[j]))
                        {
                            turretArea = allTurrets[j];
                            turretFogo = allTurrets[i];
                            synergyPossible = true;
                            break;
                        }
                    }
                }
            }
            if (synergyPossible) break;
        }

        synergyButton.gameObject.SetActive(synergyPossible);
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
        if (turretArea != null && turretFogo != null)
        {
            turretArea.SetSynergyBullet(synergyBulletPrefab);
            turretArea.SetCanShoot(true);

            turretFogo.SetCanShoot(false);
        }
    }
}
