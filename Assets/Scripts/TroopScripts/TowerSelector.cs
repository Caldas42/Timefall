using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector Instance;

    private Turret selectedTurret;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.TryGetComponent(out Turret turret))
            {
                SelectTurret(turret);
            }
            else
            {
                DeselectCurrent();
            }
        }
    }

    public void SelectTurret(Turret turret)
    {
        if (selectedTurret != null)
        {
            selectedTurret.ShowRangeIndicator(false);
        }

        selectedTurret = turret;
        selectedTurret.ShowRangeIndicator(true);
    }

    public void DeselectCurrent()
    {
        if (selectedTurret != null)
        {
            selectedTurret.ShowRangeIndicator(false);
            selectedTurret = null;
        }
    }
}
