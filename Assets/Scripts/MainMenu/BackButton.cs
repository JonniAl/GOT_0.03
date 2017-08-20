using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerDownHandler
{
    public delegate void BackButtonClick();

    public static event BackButtonClick BackClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (BackClick != null && eventData.button == PointerEventData.InputButton.Left)
        {
            BackClick();
        }
    }
}
