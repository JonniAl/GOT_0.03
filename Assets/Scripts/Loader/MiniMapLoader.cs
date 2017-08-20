using UnityEngine;
using UnityEngine.UI;

public class MiniMapLoader : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.GetTerrain() != null)
        {
            GameObject miniMapCamera = (GameObject)Instantiate(Resources.Load("Prefabs/MiniMapCamera"));

            GetComponent<Image>().sprite = miniMapCamera.GetComponent<MiniMapCamera>().GetMiniMap();

            Destroy(miniMapCamera);
        }
    }
}
