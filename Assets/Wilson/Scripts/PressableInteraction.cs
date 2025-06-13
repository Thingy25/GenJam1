using UnityEngine;

public class PressableInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Aseg�rate de que tu GameObject de jugador est� etiquetado como "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("the player has pressed the object");

            // --- �NUEVA L�NEA AQU�! ---
            // Inhabilita el GameObject al que est� adjunto este script (el cubo).
           // gameObject.SetActive(false);
        }
    }
}
