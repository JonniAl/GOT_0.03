using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Edges : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void EdgesDelegate();

    public static event EdgesDelegate UpEvent;
    public static event EdgesDelegate RightEvent;
    public static event EdgesDelegate DownEvent;
    public static event EdgesDelegate LeftEvent;

    private Image image;

    private Edge edge;

    private enum Edge
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }

    private void Awake()
    {
        edge = (Edge)Enum.Parse(typeof(Edge), name);
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (edge)
        {
            case Edge.Up:
                {
                    if (UpEvent != null)
                    {
                        UpEvent();
                    }
                    break;
                }
            case Edge.Right:
                {
                    if (RightEvent != null)
                    {
                        RightEvent();
                    }
                    break;
                }
            case Edge.Down:
                {
                    if (DownEvent != null)
                    {
                        DownEvent();
                    }
                    break;
                }
            case Edge.Left:
                {
                    if (LeftEvent != null)
                    {
                        LeftEvent();
                    }
                    break;
                }
        }

        image.raycastTarget = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.raycastTarget == false)
        {
            Debug.Log(edge);
            image.raycastTarget = true;
        }
    }
}
