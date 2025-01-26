using UnityEngine;
using UnityEngine.EventSystems; // Necesario para usar las interfaces de eventos

public class ButtonCursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D customCursor; // Cursor que aparecerá al pasar el ratón
    public Texture2D defaultCursor; // Cursor por defecto

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Cambiar al cursor personalizado al pasar sobre el botón
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Cambiar al cursor predeterminado al salir del botón
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
