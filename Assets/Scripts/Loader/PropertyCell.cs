using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertyCell : MonoBehaviour, IPointerDownHandler
{
    public delegate void PropertyCellClicked(InfoDB.PropertyAction pAction);
    public static event PropertyCellClicked PropertyCellEvent;

    private InfoDB.PropertyAction property;

    public InfoDB.PropertyCells cellIdentifier;
    
    private void Awake()
    {
        cellIdentifier = (InfoDB.PropertyCells)(int.Parse(name));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PropertyCellEvent != null)
        {
            PropertyCellEvent(property);
        }            
    }

    public void SetProperty(InfoDB.PropertyAction property)
    {
        this.property = property;
    }
}