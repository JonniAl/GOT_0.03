using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BuildingsAsset
{

    [MenuItem("Assets/Create/Buildings")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Buildings>();
    }
}
