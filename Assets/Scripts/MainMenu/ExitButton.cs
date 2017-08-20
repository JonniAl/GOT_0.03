using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerDownHandler
{
    public delegate void ExitButtonClick();

    public static event ExitButtonClick ExitClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ExitClick != null && eventData.button == PointerEventData.InputButton.Left)
        {
            ExitClick();
        }
    }
}
