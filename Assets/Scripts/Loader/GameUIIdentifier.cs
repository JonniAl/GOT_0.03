using UnityEngine;

public class GameUIIdentifier : MonoBehaviour
{
    public static GameObject InitilizeGameUI(out Camera camera)
    {
        GameObject gameInterface = (GameObject)Instantiate(Resources.Load("Prefabs/GameInterface"));

        gameInterface.name = "GameInterface";
        camera = gameInterface.GetComponentInChildren<Camera>();
        print(camera);

        return gameInterface;
    }
}
