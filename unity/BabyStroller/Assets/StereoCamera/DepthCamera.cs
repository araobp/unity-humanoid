using UnityEngine;
using System.IO;

public class DepthCamera : MonoBehaviour
{
    Material material;
    Texture2D image;

    Camera camera;
    string imageSavePath;
    RenderTexture renderTexture;

    /**
     * Capture an image from a camera
     * 
     * Note: only a main thread is allowd to perform the following operation.
     */
    public void Capture(string fileName = null)
    {
        var active = RenderTexture.active;
        camera.targetTexture = renderTexture;
        RenderTexture.active = camera.targetTexture;
        camera.Render();
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        if (fileName != null)
        {
            byte[] bytes = image.EncodeToJPG();
            File.WriteAllBytes(imageSavePath + $"/{fileName}.jpg", bytes);
        }

        camera.targetTexture = null;
        RenderTexture.active = active;       
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        camera.depthTextureMode |= DepthTextureMode.Depth;
        renderTexture = Resources.Load<RenderTexture>("Textures/DepthRenderTexture");
        image = new Texture2D(renderTexture.width, renderTexture.height);
        imageSavePath = Application.dataPath + "/CapturedImages/";

        material = Resources.Load<Material>("Materials/DepthMaterial");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Capture("captured");
        }
    }

    public void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, material);
    }
}
