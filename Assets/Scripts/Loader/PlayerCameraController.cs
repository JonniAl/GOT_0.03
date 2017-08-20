using System;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private CameraDriver gameCameraDriver;

    [SerializeField]
    private Camera gameCamera;

    public Transform plane;

    public Vector4 boundaries;

    private float height = 25;

    [SerializeField]
    private float zoomPosition = 15;
    private float zoomSpeed = 1;

    private void OnEnable()
    {
        InputController.OnButton_W_Hold += Up;
        InputController.OnButton_A_Hold += Left;
        InputController.OnButton_S_Hold += Down;
        InputController.OnButton_D_Hold += Right;
        InputController.OnScroolWheel_Up += WheelUp;
        InputController.OnScroolWheel_Down += WheelDown;

        Edges.UpEvent += Up;
        Edges.LeftEvent += Left;
        Edges.DownEvent += Down;
        Edges.RightEvent += Right;

        MouseUIScript.MouseDragEvent += MouseDrag;
    }

    private void Start()
    {
        gameCameraDriver = GameManager.GetGameCamera().gameObject.GetComponent<CameraDriver>();
        gameCamera = gameCameraDriver.gameObject.GetComponent<Camera>();

        AbstractSceneManager.ASMStartOfSceneEvent += ReloadController;
    }

    private void LateUpdate()
    {
        gameCamera.orthographicSize = zoomPosition;
        //gameCamera.orthographicSize = Mathf.Lerp(gameCamera.orthographicSize, zoomPosition, zoomSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        InputController.OnButton_W_Hold -= Up;
        InputController.OnButton_A_Hold -= Left;
        InputController.OnButton_S_Hold -= Down;
        InputController.OnButton_D_Hold -= Right;
        InputController.OnScroolWheel_Up -= WheelUp;
        InputController.OnScroolWheel_Down -= WheelDown;

        Edges.UpEvent -= Up;
        Edges.LeftEvent -= Left;
        Edges.DownEvent -= Down;
        Edges.RightEvent -= Right;

        MouseUIScript.MouseDragEvent -= MouseDrag;
    }

    private void MouseDrag(Vector3 startMousePosition, Vector3 endMousePosition)
    {
        float offsetX = startMousePosition.x - endMousePosition.x;
        float offsetY = startMousePosition.y - endMousePosition.y;
        
        Vector3 cameraSTWP = gameCamera.ScreenToWorldPoint(startMousePosition) - gameCamera.ScreenToWorldPoint(endMousePosition);

        cameraSTWP = new Vector3(cameraSTWP.x, 0, cameraSTWP.z);

        if ((offsetY > 0 && gameCamera.transform.position.z >= boundaries.x) || (offsetY < 0 && gameCamera.transform.position.z <= boundaries.z))
        {
            cameraSTWP = new Vector3(cameraSTWP.x, 0, 0);
        }

        if ((offsetX > 0 && gameCamera.transform.position.x >= boundaries.y) || (offsetX < 0 && gameCamera.transform.position.x <= boundaries.w))
        {
            cameraSTWP = new Vector3(0, 0, cameraSTWP.z);
        }

        gameCameraDriver.MoveToPositionOffsetInstantly(cameraSTWP);
    }

    private void ReloadController()
    {
        if (GameManager.GetTerrain() != null)
        {
            plane = GameManager.GetTerrain().GetComponent<Transform>(); ;
            gameCameraDriver.MoveOffset(new Vector3(0, height, 0));

            RecalculateBoundries();
        }
    }

    private void RecalculateBoundries()
    {
        float horizontalOffset = zoomPosition * gameCamera.rect.xMax * Screen.width / Screen.height + 5;

        float camRotationRad = (90 - gameCamera.transform.rotation.eulerAngles.x) * Mathf.Deg2Rad;
        
        float verticalOffsetUp = Mathf.Tan(camRotationRad) * height + zoomPosition / Mathf.Cos(camRotationRad) + 5;
        float verticalOffsetDown = Mathf.Tan(camRotationRad) * height - zoomPosition / Mathf.Cos(camRotationRad) - 5;

        float up = plane.position.z + plane.localScale.z * 5 - verticalOffsetUp;
        float right = plane.position.x + plane.localScale.x * 5 - horizontalOffset;
        float down = plane.position.z - plane.localScale.z * 5 - verticalOffsetDown;
        float left = plane.position.x - plane.localScale.x * 5 + horizontalOffset;

        boundaries = new Vector4(up, right, down, left);
    }

    private void Up()
    {
        if (gameCamera.transform.position.z < boundaries.x) { gameCameraDriver.MoveToPositionOffsetInstantly(new Vector3(0, 0, 1)); }        
    }
     
    private void Down()
    {
        if (gameCamera.transform.position.z > boundaries.z) { gameCameraDriver.MoveToPositionOffsetInstantly(new Vector3(0, 0, -1)); }
    }

    private void Left()
    {
        if (gameCamera.transform.position.x > boundaries.w) { gameCameraDriver.MoveToPositionOffsetInstantly(new Vector3(-1, 0, 0)); }
    }

    private void Right()
    {
        if (gameCamera.transform.position.x < boundaries.y) { gameCameraDriver.MoveToPositionOffsetInstantly(new Vector3(1, 0, 0)); }
    }

    private void WheelDown()
    {
        if (zoomPosition < 20)
        {
            zoomPosition++;
            RecalculateBoundries();
        }
    }

    private void WheelUp()
    {
        if (zoomPosition > 10)
        {
            zoomPosition--;
            RecalculateBoundries();
        }
    }
}
