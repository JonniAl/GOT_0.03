using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;


public class BuildingsOnScene : MonoBehaviour {

    private InfoDB.Unit unit;
    public Buildings BuildingsSettings;
    private RaycastHit hit;
    private Ray cameraRay;
    private BuildingFactoryBarrack barrackFactory;
    private GameObject build;
    private Camera cam;

    public InfoDB.Unit Unit
    {
        set
        {
            unit = value;
        }
    }


    
    // Use this for initialization
    void Start () {
        barrackFactory = new BuildingFactoryBarrack();
        BuildingsSettings = Resources.Load<Buildings>("SObjects/UnitBasicStats/New Buildings");
        cam = FindObjectOfType<CameraDriver>().GetComponent<Camera>();
        PreCreationBuild();
    }

    // Update is called once per frame
    void Update()
    {
        cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out hit))
        {

            build.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.GetComponent<GroundHit>() != null)
            {
                Destroy(build);
                barrackFactory.CreateBuild(BuildingsSettings, hit.point);
                Destroy(this);
            }
        }
    }

    void PreCreationBuild()
    {
        build = Instantiate<GameObject>(BuildingsSettings.GetBuildingByType(InfoDB.Unit.Barraks).prefabBuilding);
        int countChild = build.transform.childCount;
        for (int i = 0; i < countChild; i++)
        {
            Color temp = build.transform.GetChild(i).GetComponent<Renderer>().material.color;
            temp.a = 0.6f;
            build.transform.GetChild(i).GetComponent<Renderer>().material.color = temp;
        }
    }

}
