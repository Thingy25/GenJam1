using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    // Esta función se llama cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Podrías querer una forma más robusta de identificar a tu jugador,
        // por ejemplo, por tag, layer o un componente específico.
        // Por ahora, asumiremos que tu GameObject de jugador está etiquetado como "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("me toco");
        }
    }
}
