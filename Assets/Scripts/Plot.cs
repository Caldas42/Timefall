using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Sprite highlightSprite;

    private GameObject Troop;
    private Color startColor;

    public bool isPlaceable = true;

    private Collider2D plotCollider;
    private bool isHighlighted = false;

    private SpriteRenderer highlightSR;

    private void Start()
    {
        startColor = sr.color;
        plotCollider = GetComponent<Collider2D>();

        if (!isPlaceable && plotCollider != null)
        {
            plotCollider.enabled = false;
        }

        Transform highlightChild = transform.Find("HighlightSprite");
        if (highlightChild == null)
        {
            GameObject go = new GameObject("HighlightSprite");
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            highlightSR = go.AddComponent<SpriteRenderer>();
            highlightSR.sprite = highlightSprite;
            highlightSR.sortingLayerID = sr.sortingLayerID;
            highlightSR.sortingOrder = sr.sortingOrder + 1;
            highlightSR.color = Color.white;
            highlightSR.enabled = false;
        }
        else
        {
            highlightSR = highlightChild.GetComponent<SpriteRenderer>();
            if (highlightSR == null)
                highlightSR = highlightChild.gameObject.AddComponent<SpriteRenderer>();

            highlightSR.sprite = highlightSprite;
            highlightSR.sortingLayerID = sr.sortingLayerID;
            highlightSR.sortingOrder = sr.sortingOrder + 1;
            highlightSR.color = Color.white;
            highlightSR.enabled = false;
        }
    }


    public void PlaceTower(GameObject tower)
    {
        Troop = tower;
        isPlaceable = false;

        if (plotCollider != null)
        {
            plotCollider.enabled = false;
        }

        if (highlightSR != null)
            highlightSR.enabled = false;

        sr.color = startColor;
    }

    public void SetHighlight(bool active)
    {
        isHighlighted = active;

        if (!isPlaceable)
        {
            if (highlightSR != null)
                highlightSR.enabled = false;
            return;
        }

        if (highlightSR != null)
            highlightSR.enabled = active;

    }
}
