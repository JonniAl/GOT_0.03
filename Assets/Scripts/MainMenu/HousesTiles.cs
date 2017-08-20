using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HousesTiles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public delegate void BackButtonClick(InfoDB.House house);
    public static event BackButtonClick SideOfConflict;

    private InfoDB.House house;

    private Vector3 startingPosition;
    private Vector3 offset = new Vector3(0, 0, 100);
    private Vector3 menuOffset = new Vector3(0, 0, 250);
    private Vector3 endOfSceneOffset = new Vector3(0, 0, -1000);

    private Vector3 current;
    private float speed = 0.05f;

    private float fadeIn = 0;
    private float fadeOut = 1;

    private float fadeSpeed;
    private float transparency;

    private Image fadeOutSprite;

    private void Awake()
    {
        MainMenuManager.HouseSetted += StartGame;

        house = InfoDB.House.Lannister;

        if (GetComponent<StarkHouse>() != null) { house = InfoDB.House.Stark; }

        startingPosition = transform.localPosition;
        current = startingPosition;

        fadeOutSprite = GetComponent<Image>();

        InMainMenu();

        fadeOutSprite.color = new Color(1, 1, 1, 0);
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, current, speed);
        fadeOutSprite.color = new Color(1, 1, 1, Mathf.Lerp(fadeOutSprite.color.a, transparency, fadeSpeed * Time.deltaTime));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        current -= offset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        current += offset;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SideOfConflict != null)
        {
            SideOfConflict(house);
        }
    }

    private void InMainMenu()
    {
        fadeOutSprite.raycastTarget = false;

        MainMenuManager.Swap -= InMainMenu;
        MainMenuManager.Swap += InChoosingMenu;

        current += menuOffset;

        transparency = fadeIn;
        fadeSpeed = 2;
    }

    private void InChoosingMenu()
    {
        fadeOutSprite.raycastTarget = true;

        MainMenuManager.Swap -= InChoosingMenu;
        MainMenuManager.Swap += InMainMenu;

        current -= menuOffset;

        transparency = fadeOut;
        fadeSpeed = 0.5f;
    }

    private void StartGame()
    {
        fadeOutSprite.raycastTarget = false;

        MainMenuManager.Swap -= InChoosingMenu;
        transparency = fadeIn;
        fadeSpeed = 1f;

        current += endOfSceneOffset;
    }
}
