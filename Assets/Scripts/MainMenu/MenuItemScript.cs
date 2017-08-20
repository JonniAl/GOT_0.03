using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void PlayMenuClick();
    public static event PlayMenuClick MenuClickSound;

    private Image image;

    private bool isBackButton = false;

    private Quaternion startRotation;
    private Quaternion defaultOffset;
    private Quaternion onPointerOffset;

    private Quaternion currentRotation;

    private float rotationSpeed = 0.04f;

    private void Awake()
    {
        image = GetComponent<Image>();

        if (GetComponent<BackButton>() != null) { isBackButton = true; }

        MainMenuManager.HouseSetted += AwayFromScreen;
        MainMenuManager.ExitPressed += AwayFromScreen;
    }

    private void Start()
    {
        startRotation = transform.rotation;
        defaultOffset = Quaternion.Euler(0, 135, 0);
        onPointerOffset = Quaternion.Euler(0, 155, 0);

        if (isBackButton)
        {
            InChoosingMenu();
        }
        else
        {
            InMainMenu();
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, rotationSpeed);
    }

    private void InMainMenu()
    {
        image.raycastTarget = !image.raycastTarget;

        MainMenuManager.Swap -= InMainMenu;
        MainMenuManager.Swap += InChoosingMenu;

        currentRotation = startRotation * defaultOffset;
    }

    private void InChoosingMenu()
    {
        image.raycastTarget = !image.raycastTarget;

        MainMenuManager.Swap -= InChoosingMenu;
        MainMenuManager.Swap += InMainMenu;

        currentRotation = startRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentRotation = startRotation * onPointerOffset;

        if (MenuClickSound != null)
        {
            MenuClickSound();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.raycastTarget)
        {
            currentRotation = startRotation * defaultOffset;
        }
    }

    private void AwayFromScreen()
    {
        image.raycastTarget = false;

        MainMenuManager.Swap -= InMainMenu;
        MainMenuManager.Swap -= InChoosingMenu;

        currentRotation = startRotation;
    }
}
