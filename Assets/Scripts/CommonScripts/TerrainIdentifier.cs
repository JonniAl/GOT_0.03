using UnityEngine;

public class TerrainIdentifier : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetTerrain(gameObject);

        enabled = false;
    }
}
