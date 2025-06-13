using UnityEngine;

public class PressableInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Asegúrate de que tu GameObject de jugador esté etiquetado como "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("the player has pressed the object");

            // --- ¡NUEVA LÍNEA AQUÍ! ---
            // Inhabilita el GameObject al que está adjunto este script (el cubo).
           // gameObject.SetActive(false);
        }
    }
}
