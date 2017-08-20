using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler
{
    public delegate void StartButtonClick();

    public static event StartButtonClick StartClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (StartClick != null && eventData.button == PointerEventData.InputButton.Left)
        {
            StartClick();
        }
    }
}
