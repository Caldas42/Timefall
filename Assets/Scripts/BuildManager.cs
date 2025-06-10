using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedtroop = -1;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTroop()
    {
        if (selectedtroop == -1)
        {
            return null;
        }
        return towers[selectedtroop];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedtroop = _selectedTower;
    }

    internal Tower GetTowerByIndex(int index)
    {
        if (index >= 0 && index < towers.Length)
        {
            return towers[index];
        }

        return null;
    }
}