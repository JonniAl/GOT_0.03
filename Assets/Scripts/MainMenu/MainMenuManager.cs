using System;
using UnityEngine;

public class MainMenuManager : AbstractSceneManager
{
    public delegate void SwapMenu();
    public static event SwapMenu Swap;

    public delegate void HouseSet();
    public static event HouseSet HouseSetted;

    public delegate void ExitButtonPressed();
    public static event ExitButtonPressed ExitPressed;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip buttonSound;

    private float mute = 0;
    private float fullvolume = 1;

    private float currentVolume;

    private void Awake()
    {
        ASMAwake();

        MenuItemScript.MenuClickSound += PlayMenuMethod;
        StartButton.StartClick += SwapMenuMethod;
        BackButton.BackClick += SwapMenuMethod;
        ExitButton.ExitClick += ExitButtonMethod;
        HousesTiles.SideOfConflict += SetHouse;

        audioSource = GetComponent<AudioSource>();
        buttonSound = (AudioClip)Resources.Load("Media/ButtonSound");

        currentVolume = fullvolume;
    }

    private void Start()
    {
        ASMStart();
    }

    private void FixedUpdate()
    {
        audioSource.volume = Mathf.Lerp(audioSource.volume, currentVolume, 0.1f);
    }

    private void PlayMenuMethod()
    {
        audioSource.PlayOneShot(buttonSound, currentVolume);
    }

    private void SetHouse(InfoDB.House house)
    {
        if (HouseSetted != null)
        {
            HouseSetted();
        }

        currentVolume = mute;

        ASMEndOfSceneMethod();

        ASMSetHouseMethod(house);

        MainMenuCameraController.InPos += ASMLoadNextSceneMethod;
    }

    private void SwapMenuMethod()
    {
        if (Swap != null)
        {
            Swap();
        }
    }

    private void ExitButtonMethod()
    {
        currentVolume = mute;

        if(ExitPressed != null)
        {
            ExitPressed();
        }

        currentVolume = mute;

        ASMEndOfSceneMethod();

        MainMenuCameraController.InPos += ASMExitGameMethod;
    }

    protected override void LoadCameraFromGM()
    {
        currentCamera = GameManager.GetMenuCamera();
    }
}
