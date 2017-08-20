using UnityEngine;

public class MenuUIIdentifier : MonoBehaviour
{
    public static GameObject InitilizeMenuUI(out Camera camera)
    {
        GameObject menuInterface = (GameObject)Instantiate(Resources.Load("Prefabs/MenuInterface"));

        menuInterface.name = "MenuInterface";
        camera = menuInterface.GetComponentInChildren<Camera>();
        print(camera);

        return menuInterface;
    }
}
