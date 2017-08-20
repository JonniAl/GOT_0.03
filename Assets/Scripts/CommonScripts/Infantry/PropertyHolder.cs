using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyHolder : MonoBehaviour
{
    [SerializeField]
    private SOUnitBasicStats basicProperty;

    [SerializeField]
    private List<Property> properties;

    private int propertiesSize = 12;
    private Property emptyProperty;

    [SerializeField]
    private InfoDB.Unit unit;

    private void Start()
    {
        emptyProperty = UnitProperties.GetPropertyByAction(InfoDB.PropertyAction.Nothing);

        if (basicProperty == null)
        {
            basicProperty = Resources.Load<SOUnitBasicStats>("SObjects/UnitBasicStats/" + unit.ToString());
        }

        properties = new List<Property>(propertiesSize);

        for (int i = 0; i < propertiesSize; i++) { properties.Add(emptyProperty); }

        foreach (InfoDB.PropertyAction startingProperty in basicProperty.properties)
        {
            AddItem(startingProperty);
        }
    }

    public void AddItem(InfoDB.PropertyAction property)
    {
        Property tempProperty = UnitProperties.GetPropertyByAction(property);

        switch (tempProperty.pType)
        {
            case Property.PropertyType.Active:
                {
                    int i = 0;                 

                    while (i < 4 && properties[i] != emptyProperty) { i++; }
                    properties[i] = tempProperty;

                    break;
                }

            default:
                {
                    int i = 4;
                    while (i < 12 && properties[i] != emptyProperty) { i++; }

                    properties[i] = tempProperty;

                    break;
                }
        }
    } 

    public Property GetPropertyFromHolder(int index)
    { 
        return properties[index];
    }

    public void SetUnit(InfoDB.Unit unit)
    {
        this.unit = unit;
    }
}
