using UnityEngine;

public class PuzzleSolvedSoundPlayer : MonoBehaviour
{
    [Header("Configuraci�n de Sonido")]
    [SerializeField] private AudioSource audioSource; // Referencia al AudioSource en este GameObject
    [SerializeField] private AudioClip puzzleSolvedClip; // El clip de sonido del puzzle resuelto

    void Awake()
    {
        // Obtener el AudioSource si no est� asignado en el Inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Verificar si se encontr� el AudioSource
        if (audioSource == null)
        {
            Debug.LogError($"PuzzleSolvedSoundPlayer en {gameObject.name}: No se encontr� un AudioSource. Aseg�rate de a�adir uno.");
            this.enabled = false; // Deshabilita el script si no puede funcionar
        }

        // Opcional: Advertencia si no se asigna un clip de sonido
        if (puzzleSolvedClip == null)
        {
            Debug.LogWarning($"PuzzleSolvedSoundPlayer en {gameObject.name}: No se ha asignado un AudioClip para el sonido del puzzle resuelto.");
        }
    }

    /// <summary>
    /// Reproduce el sonido de puzzle resuelto. Este m�todo puede ser llamado por otros scripts.
    /// </summary>
    public void PlayPuzzleSolvedSound()
    {
        if (audioSource != null && puzzleSolvedClip != null)
        {
            audioSource.PlayOneShot(puzzleSolvedClip);
            Debug.Log($"Reproduciendo sonido: Puzzle Resuelto!");
        }
        else
        {
            Debug.LogWarning($"No se pudo reproducir el sonido del puzzle resuelto en {gameObject.name}. " +
                             $"AudioSource nulo: {(audioSource == null)}, " +
                             $"Clip nulo: {(puzzleSolvedClip == null)}.");
        }
    }
}