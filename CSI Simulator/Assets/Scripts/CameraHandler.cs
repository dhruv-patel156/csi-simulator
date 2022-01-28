using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Camera myCamera;
    private bool takeScreenshot;

    private void Awake() {
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender() {
        if (takeScreenshot) {
            takeScreenshot = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Photo.png", byteArray);
        }
    }

    public void TakePicture() {
        takeScreenshot = true;
    }
}
