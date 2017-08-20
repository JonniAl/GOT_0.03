using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUIScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public delegate void MouseDragDelegate(Vector3 startMousePosition, Vector3 endMousePosition);
    public static event MouseDragDelegate MouseDragEvent;

    [SerializeField]
    private Vector3 startMousePosition;
    [SerializeField]
    private Vector3 endMousePosition;

    [SerializeField]
    private GameObject selector;

    [SerializeField]
    private RectTransform selection;

    private void Awake()
    {
        selector = (GameObject)Instantiate(Resources.Load("Prefabs/MouseRectangle"));

        selection = selector.GetComponent<RectTransform>();
        selector.name = "MouseSelector";

        selector.transform.SetParent(transform);

        selector.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startMousePosition = eventData.position;        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            endMousePosition = eventData.position;

            selection.position = Vector3.Min(startMousePosition, endMousePosition);
            selection.sizeDelta = Vector3.Max(startMousePosition, endMousePosition) - selection.position;

            if (!selector.activeSelf)
            {
                selector.SetActive(true);
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right && MouseDragEvent != null)
        {
            MouseDragEvent(startMousePosition, eventData.position);
            startMousePosition = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (selector.activeSelf)
        {
            selector.SetActive(false);
        }
    }
}
