using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Camera miniMapCamera;
    public RawImage miniMapImage;
    RenderTexture renderTexture;


    void Start()
    {
        renderTexture = new RenderTexture(miniMapCamera.pixelWidth, miniMapCamera.pixelHeight, 24);
        miniMapCamera.targetTexture = renderTexture;
    }

    void LateUpdate()
    {
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        miniMapImage.texture = texture2D;
    }
}