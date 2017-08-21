using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Property
{
    public string propertyName;
    public int propertyID;
    public string propertyDescription;

    public Sprite propertySprite;

    public PropertyType pType;

   
    public enum PropertyType
    {
        Empty = 0,
        Active = 1,
        Building = 2,
        Research = 3
    }

    public InfoDB.PropertyAction pAction;

    public void SetProperty(string propertyName, int propertyID, string propertyDescription, Sprite propertySprite, PropertyType pType, InfoDB.PropertyAction pAction)
    {
        this.propertyID = propertyID;
        this.propertyName = propertyName;
        this.propertyDescription = propertyDescription;
        this.propertySprite = propertySprite;
        this.pType = pType;
        this.pAction = pAction;
    }
}
