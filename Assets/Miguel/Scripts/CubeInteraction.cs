using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    // Esta funci�n se llama cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Podr�as querer una forma m�s robusta de identificar a tu jugador,
        // por ejemplo, por tag, layer o un componente espec�fico.
        // Por ahora, asumiremos que tu GameObject de jugador est� etiquetado como "Player".
        if (other.CompareTag("Player"))
        {
            Debug.Log("me toco");
        }
    }
}
