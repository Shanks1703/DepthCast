using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class DepthCast
{
    private static RenderTexture depthMap = null;
    private static Material depthCast = null;

    public static bool RayCast(Vector2 screenPosition, out DepthCastHit hitInfo)
    {
        if (depthCast == null)
            depthCast = new Material(Shader.Find("Unlit/DepthCast"));

        if (depthMap == null)
            depthMap = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.RFloat);

        Graphics.Blit(null, depthMap, depthCast, 0);

        RenderTexture prev = RenderTexture.active;

        Texture2D tmp = new Texture2D(depthMap.width, depthMap.height, TextureFormat.RFloat, false);
        RenderTexture.active = depthMap;

        tmp.ReadPixels(new Rect(0, 0, depthMap.width, depthMap.height), 0, 0);
        tmp.Apply();

        float depth = tmp.GetPixel((int)(screenPosition.x / Screen.width * Screen.width), (int)(screenPosition.y / Screen.height * Screen.height)).r;

        if (depth == 0 || depth == 1) 
        {
            hitInfo = null;
            return false;
        }
        
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Mathf.Lerp(0, Camera.main.farClipPlane, depth)));

        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(depthMap);

        hitInfo = new DepthCastHit(worldPoint);
        return true;
    }
}

public class DepthCastHit
{
    public Vector3 position;

    public DepthCastHit(Vector3 position)
    {
        this.position = position;
    }
}
