using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactoryBarrack : BuildingFactory {

    public override GameObject CreateBuild(Buildings buildings, Vector3 position)
    {
        GameObject newBuild = GameObject.Instantiate<GameObject>(buildings.GetBuildingByType(InfoDB.Unit.Barraks).prefabBuilding, position, buildings.GetBuildingByType(InfoDB.Unit.Barraks).prefabBuilding.transform.rotation);
        newBuild.GetComponent<BoxCollider>().enabled = true;
        newBuild.AddComponent<BuildBarrack>();
        newBuild.AddComponent<Selectable>();
        newBuild.AddComponent<PropertyHolder>().SetUnit(InfoDB.Unit.Barraks);
        return newBuild;
    }

    

}
