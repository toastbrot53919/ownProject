using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class OutlineHighlight : MonoBehaviour
{
    public Material highlightMaterial;
    public Color highlightColor = Color.red;
    public float outlineThickness = 2f;
    public Transform target;

    private Camera cam;
    private CommandBuffer commandBuffer;

    private void Start()
    {
        cam = GetComponent<Camera>();
        commandBuffer = new CommandBuffer();
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (target == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        commandBuffer.Clear();

        var renderTexture = RenderTexture.GetTemporary(src.width, src.height, src.depth, src.format);

        commandBuffer.SetRenderTarget(renderTexture);

        commandBuffer.ClearRenderTarget(true, true, Color.clear);

        var meshFilter = target.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            commandBuffer.DrawMesh(meshFilter.sharedMesh, target.localToWorldMatrix, highlightMaterial);
        }

        highlightMaterial.SetColor("_OutlineColor", highlightColor);
        highlightMaterial.SetFloat("_OutlineThickness", outlineThickness);

        Graphics.ExecuteCommandBuffer(commandBuffer);

        Graphics.Blit(renderTexture, dest);

        RenderTexture.ReleaseTemporary(renderTexture);
    }
}
