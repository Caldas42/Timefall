using UnityEngine;
using TMPro;

public class TowerButtonUI : MonoBehaviour
{
    [Header("Configuration")]
    public int towerIndex;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;

    private void Start()
    {
        Tower tower = BuildManager.main.GetTowerByIndex(towerIndex);

        nameText.text = tower.name;
        costText.text = "$" + tower.cost;
    }
}