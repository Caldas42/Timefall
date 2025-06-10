using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Texture2D hoverCursor;
    public Texture2D clickCursor;
    public Vector2 hotspot = Vector2.zero;

    private bool isClicked = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked && hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Restaura para o cursor que estava antes de entrar no objeto
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
        if (clickCursor != null)
        {
            Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;
        if (hoverCursor != null && RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
        }
        else
        {
            // Restaura para o cursor que estava antes de clicar
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnDisable()
    {
        // Restaura para o cursor que estava antes de habilitar o objeto
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
