using UnityEngine;
using System.Text;
using System.Threading;

public class DepthCamera : MonoBehaviour
{

    public Material material;
    Camera depthCamera;

    public void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, material);
    }

    void Start()
    {
        depthCamera = GetComponent<Camera>();
        depthCamera.depthTextureMode |= DepthTextureMode.Depth;
    }

}
