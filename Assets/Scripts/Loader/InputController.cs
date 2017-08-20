using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void InputEvent();
    public delegate void MouseButtonEvent(Vector3 vector);

    public static event InputEvent OnButton_W_Hold;
    public static event InputEvent OnButton_A_Hold;
    public static event InputEvent OnButton_S_Hold;
    public static event InputEvent OnButton_D_Hold;
    public static event InputEvent OnScroolWheel_Up;
    public static event InputEvent OnScroolWheel_Down;
    public static event MouseButtonEvent OnLeftMouseButtonDown;
    public static event MouseButtonEvent OnLeftMouseButtonUp;
    public static event MouseButtonEvent OnLeftMouseButtonHold;
    public static event MouseButtonEvent OnRightMouseButtonDown;
    public static event MouseButtonEvent OnRightMouseButtonUp;
    public static event MouseButtonEvent OnRightMouseButtonHold;

    public static event InputEvent OnButton_LeftCtrl_Down;
    public static event InputEvent OnButton_LeftCtrl_Up;


    private InputController iController;

    void Awake()
    {
        if (iController == null)
        {
            iController = this;
        }
        else
        {
            if (iController != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (OnButton_W_Hold != null)
            {
                OnButton_W_Hold();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (OnButton_A_Hold != null)
            {
                OnButton_A_Hold();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (OnButton_S_Hold != null)
            {
                OnButton_S_Hold();
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (OnButton_D_Hold != null)
            {
                OnButton_D_Hold();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (OnScroolWheel_Up != null)
            {
                OnScroolWheel_Up();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (OnScroolWheel_Down != null)
            {
                OnScroolWheel_Down();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (OnLeftMouseButtonDown != null)
            {
                OnLeftMouseButtonDown(Input.mousePosition);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (OnLeftMouseButtonUp != null)
            {
                OnLeftMouseButtonUp(Input.mousePosition);
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (OnLeftMouseButtonHold != null)
            {
                OnLeftMouseButtonHold(Input.mousePosition);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {            
            if (OnRightMouseButtonDown != null)
            {
                OnRightMouseButtonDown(Input.mousePosition);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (OnRightMouseButtonUp != null)
            {
                OnRightMouseButtonUp(Input.mousePosition);
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (OnRightMouseButtonHold != null)
            {
                OnRightMouseButtonHold(Input.mousePosition);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (OnButton_LeftCtrl_Down != null)
            {
                OnButton_LeftCtrl_Down();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (OnButton_LeftCtrl_Up != null)
            {
                OnButton_LeftCtrl_Up();
            }
        }
    }
}
