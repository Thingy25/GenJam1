using UnityEngine;

public class ShaderSwitcher : MonoBehaviour
{
    public Shader grayscaleShader;
    public Shader invertShader;

    private Renderer[] renderers;
    private bool isInverted = false;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isInverted = !isInverted;
            ApplyShader();
        }
    }

    void ApplyShader()
    {
        Shader targetShader = isInverted ? invertShader : grayscaleShader;

        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                if (mat != null)
                    mat.shader = targetShader;
            }
        }
    }
}

