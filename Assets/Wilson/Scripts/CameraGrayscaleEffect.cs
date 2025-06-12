using UnityEngine;

// Asegura que este script siempre est� en un GameObject que tenga un componente Camera
[RequireComponent(typeof(Camera))]
public class CameraGrayscaleEffect : MonoBehaviour
{
    // Variable p�blica para arrastrar tu material GrayscaleEffectMaterial en el Inspector
    public Material grayscaleMaterial;

    // Variable p�blica controlable en el Inspector, para ajustar la intensidad de gris
    [Range(0f, 1f)] // Restringe el deslizador entre 0 (color) y 1 (gris total)
    public float currentGrayscaleAmount = 0.0f;

    // Propiedad p�blica para que otros scripts puedan cambiar el valor de forma segura
    public float GrayscaleAmount
    {
        get { return currentGrayscaleAmount; }
        set
        {
            // Clampa el valor entre 0 y 1 para evitar errores
            currentGrayscaleAmount = Mathf.Clamp01(value);
            // Si el material existe, actualiza su par�metro "_GrayscaleAmount"
            if (grayscaleMaterial != null)
            {
                grayscaleMaterial.SetFloat("_GrayscaleAmount", currentGrayscaleAmount);
            }
        }
    }

    // Se llama una vez al inicio, si el script est� activo
    void Start()
    {
        // Aseg�rate de que el material est� asignado para evitar errores en tiempo de ejecuci�n
        if (grayscaleMaterial == null)
        {
            Debug.LogError("Grayscale Material is not assigned to CameraGrayscaleEffect! Disabling script.");
            enabled = false; // Desactiva el script si no hay material
            return;
        }
        // Inicializa el shader con el valor actual desde el Inspector
        grayscaleMaterial.SetFloat("_GrayscaleAmount", currentGrayscaleAmount);
    }

    // Este m�todo se llama autom�ticamente por Unity despu�s de que la c�mara ha renderizado la escena
    // 'source' es la imagen de la c�mara antes del efecto
    // 'destination' es la imagen de la c�mara despu�s de aplicar el efecto
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Solo aplica el efecto si el material est� asignado
        if (grayscaleMaterial != null)
        {
            // Graphics.Blit copia la textura 'source' a 'destination' aplicando el 'grayscaleMaterial'
            Graphics.Blit(source, destination, grayscaleMaterial);
        }
        else
        {
            // Si no hay material, simplemente copia la imagen original sin aplicar efectos
            Graphics.Blit(source, destination);
        }
    }

    // Opcional: Se llama cuando el objeto se desactiva o se destruye.
    // �til si creas materiales din�micamente, para limpiar la memoria.
    // Para materiales arrastrados desde Project, no es estrictamente necesario,
    // ya que Unity los gestiona.
    /*
    void OnDisable()
    {
        if (grayscaleMaterial != null)
        {
            // Destruye la instancia del material para evitar fugas de memoria
            if (Application.isEditor)
                DestroyImmediate(grayscaleMaterial);
            else
                Destroy(grayscaleMaterial);
        }
    }
    */
}