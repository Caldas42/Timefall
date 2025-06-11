using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyManager : MonoBehaviour
{
    [SerializeField] private GameObject synergyBulletPrefab;
    [SerializeField] private Button synergyButton;   // botão já no canvas, referência única

    private Turret[] allTurrets;
    private List<(Turret, Turret)> synergyPairs = new List<(Turret, Turret)>();
    private List<Button> synergyButtons = new List<Button>();

    private void Start()
    {
        synergyButton.gameObject.SetActive(false);  // Usado como modelo (clone)
    }

    private void Update()
    {
        allTurrets = Object.FindObjectsByType<Turret>(FindObjectsSortMode.None);

        synergyPairs.Clear();

        for (int i = 0; i < allTurrets.Length; i++)
        {
            for (int j = i + 1; j < allTurrets.Length; j++)
            {
                if (Vector2.Distance(allTurrets[i].transform.position, allTurrets[j].transform.position) < 1.5f)
                {
                    if (!allTurrets[i].HasSinergia() && !allTurrets[j].HasSinergia())
                    {
                        Turret turretArea = null;
                        Turret turretFogo = null;

                        if (IsAreaTurret(allTurrets[i]) && IsFireTurret(allTurrets[j]))
                        {
                            turretArea = allTurrets[i];
                            turretFogo = allTurrets[j];
                        }
                        else if (IsFireTurret(allTurrets[i]) && IsAreaTurret(allTurrets[j]))
                        {
                            turretArea = allTurrets[j];
                            turretFogo = allTurrets[i];
                        }

                        if (turretArea != null && turretFogo != null)
                        {
                            synergyPairs.Add((turretArea, turretFogo));
                        }
                    }
                }
            }
        }

        UpdateSynergyButtons();
    }

    private void UpdateSynergyButtons()
    {
        // Ajusta a quantidade de botões instanciados
        while (synergyButtons.Count < synergyPairs.Count)
        {
            Button newButton = Instantiate(synergyButton, synergyButton.transform.parent);
            newButton.gameObject.SetActive(false);
            synergyButtons.Add(newButton);
        }

        // Ativa e posiciona apenas os necessários
        for (int i = 0; i < synergyButtons.Count; i++)
        {
            if (i < synergyPairs.Count)
            {
                synergyButtons[i].gameObject.SetActive(true);
                var pair = synergyPairs[i];

                // Posiciona o botão acima da torre de área
                Vector3 screenPos = Camera.main.WorldToScreenPoint(pair.Item1.transform.position + Vector3.up * 1f);
                synergyButtons[i].transform.position = screenPos;

                // Remove listeners antigos para evitar empilhamento
                synergyButtons[i].onClick.RemoveAllListeners();

                // Adiciona listener específico para esta dupla
                synergyButtons[i].onClick.AddListener(() =>
                {
                    ActivateSynergy(pair.Item1, pair.Item2);
                });
            }
            else
            {
                synergyButtons[i].gameObject.SetActive(false);
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

    private void ActivateSynergy(Turret turretArea, Turret turretFogo)
    {
        if (turretArea != null && turretFogo != null)
        {
            turretArea.SetSynergyBullet(synergyBulletPrefab);
            turretArea.SetCanShoot(true);

            turretFogo.SetCanShoot(false);
        }
    }
}
