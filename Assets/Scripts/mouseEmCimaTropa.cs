using UnityEngine;

public class ShowRangeOnHover : MonoBehaviour
{
    public GameObject rangeIndicator;

    void Start()
    {
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
        }
    }
}
