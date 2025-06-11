using UnityEngine;
using UnityEngine.EventSystems;

public class DragTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject previewTower;
    private Tower towerData;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        var ui = GetComponent<TowerButtonUI>();
        towerData = BuildManager.main.GetTowerByIndex(ui.towerIndex);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (towerData == null || towerData.cost > LevelManager.main.getCurrency())
        {
            Debug.Log("Sem torre ou sem moeda.");
            return;
        }

        previewTower = Instantiate(towerData.prefab);
        previewTower.GetComponent<Turret>().SetCanShoot(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (previewTower == null) return;

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        previewTower.transform.position = worldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (previewTower == null) return;

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.TryGetComponent(out Plot plot) && plot.isPlaceable)
        {
            if (towerData.cost <= LevelManager.main.getCurrency())
            {
                LevelManager.main.SpendCurrency(towerData.cost);

                GameObject finalTower = Instantiate(towerData.prefab, plot.transform.position, Quaternion.identity);
                finalTower.GetComponent<Turret>().SetCanShoot(true);

                plot.PlaceTower(finalTower);
            }
            else
            {
                Debug.Log("Moeda insuficiente.");
            }
        }

        Destroy(previewTower);
    }
}