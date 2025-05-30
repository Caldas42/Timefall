using UnityEngine;
using UnityEngine.UI;

public class SynergyManager : MonoBehaviour
{
    [SerializeField] private GameObject synergyBulletPrefab;
    [SerializeField] private Button synergyButton;   // botão já no canvas

    private Turret[] allTurrets;

    private Turret turretArea;
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

        turretArea = null;
        turretFogo = null;

        for (int i = 0; i < allTurrets.Length; i++)
        {
            for (int j = i + 1; j < allTurrets.Length; j++)
            {
                if (Vector2.Distance(allTurrets[i].transform.position, allTurrets[j].transform.position) < 1.5f)
                {
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

        if (synergyPossible)
        {
            // Posiciona o botão em cima da torre area (ajuste o Vector3.up * offset se quiser mais pra cima)
            Vector3 screenPos = Camera.main.WorldToScreenPoint(turretArea.transform.position + Vector3.up * 1f);
            synergyButton.transform.position = screenPos;
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
        if (turretArea != null && turretFogo != null)
        {
            turretArea.SetSynergyBullet(synergyBulletPrefab);
            turretArea.SetCanShoot(true);

            turretFogo.SetCanShoot(false);
        }
    }
}
