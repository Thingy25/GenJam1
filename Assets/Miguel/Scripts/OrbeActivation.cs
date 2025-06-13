using UnityEngine;

public class OrbeActivation : MonoBehaviour
{
    private bool activate = false;
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el collider que entra pertenece al jugador.
        // Aseg�rate de que tu GameObject de jugador est� etiquetado como "Player".
        if (other.CompareTag("Core"))
        {
            activate = true;
            Debug.Log("the Core has pressed the object");

        }
    }
}
