using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class DepthCastMask : MonoBehaviour
{
    public LayerMask mask;
    private RenderTexture target;
    private Camera currentCamera;
    private Vector2Int previousScaling;

    void Start()
    {
        RenderPipelineManager.endCameraRendering += OnCameraPostRender;
        RenderPipelineManager.beginCameraRendering += OnCameraPreRender;
        target = RenderTexture.GetTemporary(Screen.width, Screen.height);
        currentCamera = GetComponent<Camera>();
        currentCamera.targetTexture = target;
        currentCamera.cullingMask = mask;
        currentCamera.allowMSAA = false;
        currentCamera.clearFlags = CameraClearFlags.Color;
        currentCamera.backgroundColor = new Color(0, 0, 0, 0);
        currentCamera.depth = -999;
        UniversalAdditionalCameraData addCamData = GetComponent<UniversalAdditionalCameraData>();
        addCamData.renderShadows = false;
        addCamData.renderPostProcessing = false;
        previousScaling = new Vector2Int(Screen.width, Screen.height);
    }

    void OnCameraPreRender(ScriptableRenderContext ctx, Camera cam)
    {
        if (previousScaling != new Vector2Int(Screen.width, Screen.height))
        {
            RenderTexture.ReleaseTemporary(target);
            target = RenderTexture.GetTemporary(Screen.width, Screen.height);
            currentCamera.targetTexture = target;
            previousScaling = new Vector2Int(Screen.width, Screen.height);
        }
    }

    void OnCameraPostRender(ScriptableRenderContext ctx, Camera cam)
    {
        Shader.SetGlobalTexture("_DepthMask", target);
    }

    void OnDestroy()
    {
        RenderPipelineManager.endCameraRendering -= OnCameraPostRender;
        RenderPipelineManager.beginCameraRendering -= OnCameraPreRender;
    }
}
