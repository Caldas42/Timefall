using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject Troop;
    private Color startColor;

    public bool isPlaceable = true;

    private Collider2D plotCollider;

    private void Start()
    {
        startColor = sr.color;
        plotCollider = GetComponent<Collider2D>();

        if (!isPlaceable && plotCollider != null)
        {
            plotCollider.enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        if (isPlaceable)
        {
            sr.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    public void PlaceTower(GameObject tower)
    {
        Troop = tower;
        isPlaceable = false;

        if (plotCollider != null)
        {
            plotCollider.enabled = false;
        }
    }
}
