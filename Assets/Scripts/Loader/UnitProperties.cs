using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProperties : MonoBehaviour
{
    public static Dictionary<InfoDB.PropertyAction, Property> propertyDictionary = new Dictionary<InfoDB.PropertyAction, Property>();

    private void Awake()
    {
        Property empty = new Property();
        empty.SetProperty("Empty", 0, "Empty property for panel" ,Resources.Load<Sprite>("Sprites/PropertiesSprites/EmptyProperty"), Property.PropertyType.Empty, InfoDB.PropertyAction.Nothing);

        int i = 0;

        propertyDictionary.Add(InfoDB.PropertyAction.Nothing, empty);

        foreach (InfoDB.PropertyAction prop in InfoDB.aProperties)
        {
            Property action = new Property();

            action.SetProperty(prop.ToString(), ++i, prop.ToString() + " property for panel", Resources.Load<Sprite>("Sprites/PropertiesSprites/" + prop.ToString()), Property.PropertyType.Active, prop);

            propertyDictionary.Add(prop, action);
        }

        foreach (InfoDB.PropertyAction prop in InfoDB.bProperties)
        {
            Property building = new Property();

            building.SetProperty(prop.ToString(), ++i, prop.ToString() + " property for panel", Resources.Load<Sprite>("Sprites/PropertiesSprites/" + prop.ToString()), Property.PropertyType.Building, prop);

            propertyDictionary.Add(prop, building);
        }

        foreach (InfoDB.PropertyAction prop in InfoDB.rProperties)
        {
            Property research = new Property();

            research.SetProperty(prop.ToString(), ++i, prop.ToString() + " property for panel", Resources.Load<Sprite>("Sprites/PropertiesSprites/" + prop.ToString()), Property.PropertyType.Research, prop);

            propertyDictionary.Add(prop, research);
        }
    }

    public static Property GetPropertyByAction(InfoDB.PropertyAction action)
    {
        return propertyDictionary[action];
    }
}
