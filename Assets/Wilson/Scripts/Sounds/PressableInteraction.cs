using UnityEngine;

public class PressableInteraction : MonoBehaviour
{
    // --- NUEVAS VARIABLES PARA EL SONIDO ---
    [Header("Configuraci�n de Sonido")]
    [SerializeField] private AudioSource interactionAudioSource; // Referencia al AudioSource en este mismo GameObject
    [SerializeField] private AudioClip pressSoundClip;           // El clip de audio que se reproducir�

    void Awake()
    {
        // Si no se asign� en el Inspector, intenta obtener el AudioSource del mismo GameObject
        if (interactionAudioSource == null)
        {
            interactionAudioSource = GetComponent<AudioSource>();
        }

        // Verifica si se encontr� el AudioSource
        if (interactionAudioSource == null)
        {
            Debug.LogError($"PressableInteraction en {gameObject.name}: No se encontr� un AudioSource. Aseg�rate de a�adir uno al Prefab.");
            this.enabled = false; // Deshabilita el script si no puede funcionar
        }

        // Opcional: Advertencia si no se asigna un clip de sonido
        if (pressSoundClip == null)
        {
            Debug.LogWarning($"PressableInteraction en {gameObject.name}: No se ha asignado un AudioClip para el sonido de presi�n.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Aseg�rate de que tu GameObject de jugador est� etiquetado como "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("the player has pressed the object");

            // --- REPRODUCIR EL SONIDO AQU� ---
            // Asegurarse de que tenemos un AudioSource y un AudioClip asignados
            if (interactionAudioSource != null && pressSoundClip != null)
            {
                // PlayOneShot es perfecto para sonidos de un solo disparo, no interrumpe otros sonidos
                interactionAudioSource.PlayOneShot(pressSoundClip);
                Debug.Log($"SONIDO PARA PRESIONABLE"+"Reproduciendo sonido de presi�n para {gameObject.name}.");
            }
            else
            {
                // Mensaje de depuraci�n si falta algo para el sonido
                Debug.LogWarning($"No se pudo reproducir el sonido de presi�n en {gameObject.name}. " +
                                 $"AudioSource nulo: {(interactionAudioSource == null)}, " +
                                 $"Clip nulo: {(pressSoundClip == null)}.");
            }

            // La l�nea para inhabilitar el GameObject que ten�as, si la necesitas:
            // gameObject.SetActive(false);
        }
    }
}