using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private GameObject plane;

    private Vector3 planePosition;
    private Vector3 planeSize;
    private float planeLargerSide;

    private int textureSize = 512;
    private Camera miniMapCamera;

    public RenderTexture tempRT;
    public Texture2D miniMapSnapShot;
    public Rect miniMapRect;

    private void Awake()
    {
        plane = GameManager.GetTerrain();

        planePosition = plane.transform.position;
        planeSize = plane.transform.localScale * 10;

        planeLargerSide = planeSize.x;

        if (planeSize.z > planeSize.x)
        {
            planeLargerSide = planeSize.z;
        }
        
        miniMapCamera = GetComponent<Camera>();
        miniMapCamera.aspect = 1;

        miniMapCamera.transform.position = planePosition + new Vector3(0, planeSize.y + 100, 0);
        miniMapCamera.nearClipPlane = 0;
        miniMapCamera.farClipPlane = miniMapCamera.transform.position.y + 2;

        miniMapCamera.orthographicSize = planeLargerSide / 2;
        miniMapCamera.rect = new Rect(0, 0, planeLargerSide / 2, planeLargerSide / 2);

        tempRT = new RenderTexture(textureSize, textureSize, 24);
        miniMapSnapShot = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
        miniMapRect = new Rect(0, 0, textureSize, textureSize);
    }

    public Sprite GetMiniMap()
    {
        RenderTexture.active = tempRT;

        miniMapCamera.targetTexture = tempRT;    
        miniMapCamera.Render();               

        miniMapSnapShot.ReadPixels(miniMapRect, 0, 0);
        miniMapSnapShot.Apply();

        RenderTexture.active = null;
        miniMapCamera.targetTexture = null;

        DestroyImmediate(tempRT);

        Sprite miniMap = Sprite.Create(miniMapSnapShot, miniMapRect, new Vector2(textureSize / 2, textureSize / 2));
        miniMap.name = "MiniMapSprite";
        
        return miniMap;
    }
}
