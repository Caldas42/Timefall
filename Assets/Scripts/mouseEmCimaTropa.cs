using UnityEngine;

public class ShowRangeOnClick : MonoBehaviour
{
    public GameObject rangeIndicator;

    void Start()
    {
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (rangeIndicator != null)
        {
            bool isActive = rangeIndicator.activeSelf;
            rangeIndicator.SetActive(!isActive);
        }
    }
}
