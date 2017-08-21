using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : ScriptableObject
{
    public List<BuildingParam> BuildingElements;
	
    public BuildingParam GetBuildingByType(InfoDB.Unit typeBuilding)
    {
        for (int i = 0; i < BuildingElements.Count; i++)
        {
            if (typeBuilding == BuildingElements[i].unit)
            {
                return BuildingElements[i];
            }
        }
        return null;
    }
}
