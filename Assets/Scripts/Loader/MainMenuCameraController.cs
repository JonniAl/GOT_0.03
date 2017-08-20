using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    public delegate void InPosition();
    public static event InPosition InPos;

    [SerializeField]
    private CameraDriver menuCameraDriver;

    [SerializeField]
    private Vector3 startingPosition;
    private Vector3 offset = new Vector3(0, 0, 10);

    [SerializeField]
    private Vector3 current;

    [SerializeField]
    private bool inPosition = true;

    private void Awake()
    {
        menuCameraDriver = GameManager.GetMenuCamera().gameObject.GetComponent<CameraDriver>();
        transform.rotation = Quaternion.Euler(0, 0, 0);

        MainMenuManager.HouseSetted += StartPressed;
        MainMenuManager.ExitPressed += ExitPressed;
    }

    private void Start()
    {
        startingPosition = transform.position;

        InMainMenu();
    }

    private void FixedUpdate()
    {
        menuCameraDriver.MoveToPosition(current);

        if (!inPosition && Mathf.Abs(menuCameraDriver.transform.position.z - current.z) < 0.1f)
        {
            inPosition = true;
            Debug.Log("In Position");

            if (InPos != null)
            {
                InPos();
                InPos = null;
            }
        }
    }

    private void InMainMenu()
    {
        MainMenuManager.Swap -= InMainMenu;
        MainMenuManager.Swap += InChoosingMenu;
        current = startingPosition;

        inPosition = false;
    }

    private void InChoosingMenu()
    {
        MainMenuManager.Swap -= InChoosingMenu;
        MainMenuManager.Swap += InMainMenu;
        current = startingPosition + offset;

        inPosition = false;
    }

    private void StartPressed()
    {
        MainMenuManager.Swap -= InChoosingMenu;
        current = startingPosition + offset * 3.5f;

        inPosition = false;
    }

    private void ExitPressed()
    {
        MainMenuManager.Swap -= InMainMenu;
        current = startingPosition - offset * 4f;

        inPosition = false;
    }
}
