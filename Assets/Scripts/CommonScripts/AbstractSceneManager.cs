using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSceneManager : MonoBehaviour
{    
    public delegate void LoadNextSceneEvent();
    public static event LoadNextSceneEvent ASMLoadNextSceneEvent;

    public delegate void LoadSceneEvent(InfoDB.Scenes scene);
    public static event LoadSceneEvent ASMLoadSceneEvent;

    public delegate void StartSceneEvent();
    public static event StartSceneEvent ASMStartOfSceneEvent;

    public delegate void SceneEndEvent();
    public static event SceneEndEvent ASMEndOfSceneEvent;

    public delegate void ExitApplicationEvent();
    public static event ExitApplicationEvent ASMExitGameEvent;

    public delegate void SetHouseEvent(InfoDB.House house);
    public static event SetHouseEvent ASMSetHouseEvent;

    protected Camera currentCamera;

    [SerializeField]
    protected List<Vector3> spawningPointPosition;

    protected void ASMAwake()
    {
        LoadCameraFromGM();

        GameManager.SetCurrentSceneManager(this);

        spawningPointPosition = SpawningPoint.GetSpawningPointsPosition();

        int i = Random.Range(0, spawningPointPosition.Count - 1);

        Debug.Log(spawningPointPosition[i]);

        currentCamera.gameObject.GetComponent<CameraDriver>().MoveToPositionInstantly(spawningPointPosition[i]);

        spawningPointPosition.RemoveAt(i);
    }

    protected void ASMStart()
    {       
        ASMStartOfSceneMethod();
    }

    protected abstract void LoadCameraFromGM();

    protected virtual void ASMLoadNextSceneMethod()
    {
        if (ASMLoadNextSceneEvent != null)
        {
            Debug.Log("Load Of Next Scene");
            ASMLoadNextSceneEvent();
        }
    }

    protected virtual void ASMLoadSceneMethod(InfoDB.Scenes scene)
    {
        if (ASMLoadSceneEvent != null)
        {
            Debug.Log("Load Of Scene");
            ASMLoadSceneEvent(scene);
        }
    }

    protected virtual void ASMStartOfSceneMethod()
    {
        if (ASMStartOfSceneEvent != null)
        {
            Debug.Log("Start Of Scene");
            ASMStartOfSceneEvent();
        }
    }

    protected virtual void ASMEndOfSceneMethod()
    {
        if (ASMEndOfSceneEvent != null)
        {
            Debug.Log("End Of Scene");
            ASMEndOfSceneEvent();
        }
    }

    protected virtual void ASMExitGameMethod()
    {
        if (ASMExitGameEvent != null)
        {
            Debug.Log("Exit Game");
            ASMExitGameEvent();
        }
    }

    protected virtual void ASMSetHouseMethod(InfoDB.House house)
    {
        if (ASMSetHouseEvent != null)
        {
            ASMSetHouseEvent(house);
        }
    }
}