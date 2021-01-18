using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class DepthCamera : MonoBehaviour
{
    public RawImage rawImage;

    Material m_material;

    Texture2D m_initialImage;
    Texture2D m_accidentImage;

    Camera m_camera;
    string m_imageSavePath;
    RenderTexture m_renderTexture;

    // Start is called before the first frame update
    void Start()
    {
        // Use the camer as a depth camera
        m_camera = GetComponent<Camera>();
        m_camera.depthTextureMode |= DepthTextureMode.Depth;
        m_material = Resources.Load<Material>("Materials/DepthMaterial");

        // Image capture data
        m_renderTexture = Resources.Load<RenderTexture>("Textures/DepthRenderTexture");
        m_initialImage = new Texture2D(m_renderTexture.width, m_renderTexture.height);
        m_accidentImage = new Texture2D(m_renderTexture.width, m_renderTexture.height);
        m_imageSavePath = Application.dataPath + "/CapturedImages/";

        // Initialization
        StartCoroutine(Init());
    }

    // Take an initial depth image 
    IEnumerator Init()
    {
        yield return new WaitForSeconds(1F);  // One second after start up
        Color[] pixels = Capture("capture");
        m_initialImage.SetPixels(pixels);
        m_initialImage.Apply();

        // Show the initial depth image on the screen
        rawImage.GetComponent<RawImage>().texture = m_initialImage;
    }

    /**
    * Capture an image from a camera
    * 
    * Note: only a main thread is allowd to perform the following operation.
    */
    public Color[] Capture(string fileName = null)
    {
        Texture2D image = new Texture2D(m_renderTexture.width, m_renderTexture.height);

        var active = RenderTexture.active;
        m_camera.targetTexture = m_renderTexture;
        RenderTexture.active = m_camera.targetTexture;
        m_camera.Render();
        image.ReadPixels(new Rect(0, 0, m_renderTexture.width, m_renderTexture.height), 0, 0);
        image.Apply();

        if (fileName != null)
        {
            byte[] bytes = image.EncodeToJPG();
            File.WriteAllBytes(m_imageSavePath + $"/{fileName}.jpg", bytes);
        }

        m_camera.targetTexture = null;
        RenderTexture.active = active;

        Color[] pixels = image.GetPixels();
        Destroy(image);

        return pixels;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Capture("stuckInTheDoor");
        }
        else if(Input.GetKeyDown(KeyCode.D)) {
        //else {
            Color[] pixels = Capture();
            m_accidentImage.SetPixels(pixels);
            Color[] initialPixels = m_initialImage.GetPixels();

            for(int idx=0; idx<pixels.Length; idx++)
            {
                Color ic = initialPixels[idx];
                Color c = pixels[idx];
                float dr = c.r - ic.r;
                float dg = c.g - ic.g;
                float db = c.b - ic.b;
                if (dr < 0F) dr = 0F;
                if (dg < 0F) dg = 0F;
                if (db < 0F) db = 0F;
                pixels[idx] = new Color(dr, dg, db);
            }

            m_accidentImage.SetPixels(pixels);
            m_accidentImage.Apply();

            rawImage.GetComponent<RawImage>().texture = m_accidentImage;
        }
    }

    public void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, m_material);
    }
}
