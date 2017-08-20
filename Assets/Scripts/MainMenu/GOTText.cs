using UnityEngine;

public class GOTText : MonoBehaviour
{
    private Vector3 upCoordinate;
    private Vector3 offset = new Vector3(0, 200, 0);

    private Vector3 currentCoordinate;
    private float speed = 0.05f;

    private void Awake()
    {
        upCoordinate = transform.localPosition;

        Show();

        MainMenuManager.ExitPressed += Hide;
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, currentCoordinate, speed);
    }

    private void Show()
    {
        MainMenuManager.Swap -= Show;
        MainMenuManager.Swap += Hide;
        currentCoordinate = upCoordinate - offset;
    }

    private void Hide()
    {
        MainMenuManager.Swap -= Hide;
        MainMenuManager.Swap += Show;
        currentCoordinate = upCoordinate;
    }
}
