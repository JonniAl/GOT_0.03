using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitBasicStats", menuName = "Scriptable Objects/Unit Basic Stats", order = 1)]
public class SOUnitBasicStats : ScriptableObject
{
    public InfoDB.Unit unit;    
    public float movementSpeed = 100;

    public float attack = 10;

    public float health = 100;
    public float armor = 10;

    public List<InfoDB.PropertyAction> properties = new List<InfoDB.PropertyAction>();
}