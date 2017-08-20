using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void FadeIn();
    public static event FadeIn FaderIn;

    public delegate void FadeOut();
    public static event FadeOut FaderOut;

    private static GameManager gameManager = null;

    [SerializeField]
    private InfoDB.Scenes debugLoadingScene;

    [SerializeField]
    private GameObject currentLevelPlane;

    [SerializeField]
    private InfoDB.Scenes scene;

    [SerializeField]
    private GameObject menuInterface;

    [SerializeField]
    private Camera menuInterfaceCamera;

    [SerializeField]
    private GameObject gameInterface;

    [SerializeField]
    private Camera gameInterfaceCamera;

    [SerializeField]
    private AbstractSceneManager currentSceneManager;

    [SerializeField]
    private GameObject cameraControllers;

    [SerializeField]
    public InfoDB.House house;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            if (gameManager != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        AbstractSceneManager.ASMLoadNextSceneEvent += LoadNextScene;
        AbstractSceneManager.ASMLoadSceneEvent += LoadScene;

        AbstractSceneManager.ASMStartOfSceneEvent += StartOfScene;
        AbstractSceneManager.ASMEndOfSceneEvent += EndOfScene;

        AbstractSceneManager.ASMExitGameEvent += ExitGame;

        AbstractSceneManager.ASMSetHouseEvent += SetHouse;

        gameManager.scene = debugLoadingScene;

        Fader.InitializeFader(); 

        gameManager.gameInterface = GameUIIdentifier.InitilizeGameUI(out gameInterfaceCamera);
        gameManager.menuInterface = MenuUIIdentifier.InitilizeMenuUI(out menuInterfaceCamera);

        gameManager.cameraControllers = CameraControllersInitialize();

        LoadScene(gameManager.scene);
    }

    private void Start()
    {

    }

    protected virtual GameObject CameraControllersInitialize()
    {
        GameObject cameraControllers = (GameObject)Instantiate(Resources.Load("Prefabs/CameraControllers"));

        cameraControllers.name = "CameraControllers";

        return cameraControllers;
    }

    public static GameObject GetCurrentCameraDriver()
    {
        return gameManager.cameraControllers;
    }

    public static Camera GetMenuCamera()
    {
        return gameManager.menuInterfaceCamera;
    }

    public static Camera GetGameCamera()
    {
        return gameManager.gameInterfaceCamera;
    }

    private static void SetSceneSettings()
    {  
        switch (gameManager.scene)
        {
            case InfoDB.Scenes.EndGame:
                {
                    gameManager.cameraControllers.GetComponent<EndSceneCameraController>().enabled = true;
                    gameManager.cameraControllers.GetComponent<MainMenuCameraController>().enabled = false;
                    gameManager.cameraControllers.GetComponent<PlayerCameraController>().enabled = false;

                    gameManager.cameraControllers.transform.rotation = Quaternion.Euler(0, 0, 0);

                    gameManager.gameInterface.SetActive(false);
                    gameManager.menuInterface.SetActive(false);
                    break;
                }

            case InfoDB.Scenes.MainMenu:
                {
                    gameManager.cameraControllers.GetComponent<EndSceneCameraController>().enabled = false;
                    gameManager.cameraControllers.GetComponent<MainMenuCameraController>().enabled = true;
                    gameManager.cameraControllers.GetComponent<PlayerCameraController>().enabled = false;

                    gameManager.cameraControllers.transform.rotation = Quaternion.Euler(0, 0, 0);                    

                    gameManager.gameInterface.SetActive(false);
                    gameManager.menuInterface.SetActive(true);
                    break;
                }
            default:
                {
                    gameManager.cameraControllers.GetComponent<EndSceneCameraController>().enabled = false;
                    gameManager.cameraControllers.GetComponent<MainMenuCameraController>().enabled = false;
                    gameManager.cameraControllers.GetComponent<PlayerCameraController>().enabled = true;

                    //gameManager.cameraDriver.transform.rotation = Quaternion.Euler(65, 0, 0);

                    gameManager.gameInterface.SetActive(true);
                    gameManager.menuInterface.SetActive(false);
                    break;
                }           
        }
    }

    private static void LoadNextScene()
    {
        InfoDB.Scenes nextScene = (InfoDB.Scenes)((int)gameManager.scene + 1);

        LoadScene(nextScene);
    }

    private static void LoadScene(InfoDB.Scenes scene)
    {
        Debug.Log("Loading scene: " + scene);
        gameManager.scene = scene;

        gameManager.currentSceneManager = null;
        SpawningPoint.ClearSpawningPoints();

        SceneManager.LoadScene(gameManager.scene.ToString());
    }

    private static void StartOfScene()
    {
        SetSceneSettings();

        if (FaderOut != null)
        {
            FaderOut();
        }
    }

    private static void EndOfScene()
    {
        if (FaderIn != null)
        {
            FaderIn();
        }
    }

    private void ExitGame()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }

    private void SetHouse(InfoDB.House house)
    {
        gameManager.house = house;
    }

    public static void SetTerrain(GameObject plane)
    {
        gameManager.currentLevelPlane = plane;
    }

    public static GameObject GetTerrain()
    {
        return gameManager.currentLevelPlane;
    }

    public static void SetCurrentSceneManager(AbstractSceneManager sceneManager)
    {
        gameManager.currentSceneManager = sceneManager;
    }

    public static AbstractSceneManager GetCurrentSceneManager()
    {
        return gameManager.currentSceneManager;
    }
}