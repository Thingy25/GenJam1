using UnityEngine;

public class PressableInteraction : MonoBehaviour
{
    // --- NUEVAS VARIABLES PARA EL SONIDO ---
    [Header("Configuración de Sonido")]
    [SerializeField] private AudioSource interactionAudioSource; // Referencia al AudioSource en este mismo GameObject
    [SerializeField] private AudioClip pressSoundClip;           // El clip de audio que se reproducirá

    void Awake()
    {
        // Si no se asignó en el Inspector, intenta obtener el AudioSource del mismo GameObject
        if (interactionAudioSource == null)
        {
            interactionAudioSource = GetComponent<AudioSource>();
        }

        // Verifica si se encontró el AudioSource
        if (interactionAudioSource == null)
        {
            Debug.LogError($"PressableInteraction en {gameObject.name}: No se encontró un AudioSource. Asegúrate de añadir uno al Prefab.");
            this.enabled = false; // Deshabilita el script si no puede funcionar
        }

        // Opcional: Advertencia si no se asigna un clip de sonido
        if (pressSoundClip == null)
        {
            Debug.LogWarning($"PressableInteraction en {gameObject.name}: No se ha asignado un AudioClip para el sonido de presión.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Asegúrate de que tu GameObject de jugador esté etiquetado como "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("the player has pressed the object");

            // --- REPRODUCIR EL SONIDO AQUÍ ---
            // Asegurarse de que tenemos un AudioSource y un AudioClip asignados
            if (interactionAudioSource != null && pressSoundClip != null)
            {
                // PlayOneShot es perfecto para sonidos de un solo disparo, no interrumpe otros sonidos
                interactionAudioSource.PlayOneShot(pressSoundClip);
                Debug.Log($"SONIDO PARA PRESIONABLE"+"Reproduciendo sonido de presión para {gameObject.name}.");
            }
            else
            {
                // Mensaje de depuración si falta algo para el sonido
                Debug.LogWarning($"No se pudo reproducir el sonido de presión en {gameObject.name}. " +
                                 $"AudioSource nulo: {(interactionAudioSource == null)}, " +
                                 $"Clip nulo: {(pressSoundClip == null)}.");
            }

            // La línea para inhabilitar el GameObject que tenías, si la necesitas:
            // gameObject.SetActive(false);
        }
    }
}