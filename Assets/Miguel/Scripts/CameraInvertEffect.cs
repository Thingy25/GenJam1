
using UnityEngine;

[ExecuteInEditMode] // Permite ver el efecto en el editor
public class CameraInvertEffect : MonoBehaviour
{
    public Material invertMaterial; // Arrastra tu PostProcessInvertMaterial aqu� en el Inspector
    public bool enableInvert = false; // Control para activar/desactivar la inversi�n

    // Se llama despu�s de que la c�mara termina de renderizar la escena.
    // source: La textura de la pantalla antes de este efecto.
    // destination: La textura a la que este efecto deber�a renderizar.
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (enableInvert && invertMaterial != null)
        {
            // Aplica el shader de inversi�n al resultado de la c�mara
            Graphics.Blit(source, destination, invertMaterial);
        }
        else
        {
            // Si la inversi�n no est� activada o el material no est� asignado,
            // simplemente copia la fuente al destino sin modificaciones.
            Graphics.Blit(source, destination);
        }
    }
}