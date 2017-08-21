using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingFactory
{
    public abstract GameObject CreateBuild(Buildings buildings, Vector3 position);
}
