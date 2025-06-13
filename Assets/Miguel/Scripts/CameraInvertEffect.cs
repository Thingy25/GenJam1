
using UnityEngine;

[ExecuteInEditMode] // Permite ver el efecto en el editor
public class CameraInvertEffect : MonoBehaviour
{
    public Material invertMaterial; // Arrastra tu PostProcessInvertMaterial aquí en el Inspector
    public bool enableInvert = false; // Control para activar/desactivar la inversión

    // Se llama después de que la cámara termina de renderizar la escena.
    // source: La textura de la pantalla antes de este efecto.
    // destination: La textura a la que este efecto debería renderizar.
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (enableInvert && invertMaterial != null)
        {
            // Aplica el shader de inversión al resultado de la cámara
            Graphics.Blit(source, destination, invertMaterial);
        }
        else
        {
            // Si la inversión no está activada o el material no está asignado,
            // simplemente copia la fuente al destino sin modificaciones.
            Graphics.Blit(source, destination);
        }
    }
}