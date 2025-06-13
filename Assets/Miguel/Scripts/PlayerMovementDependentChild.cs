using UnityEngine;
using UnityEngine.InputSystem; 


public class PlayerMovementDependentChild : MonoBehaviour
{
    
    public Transform childToMove;

    [Tooltip("La fuerza/distancia del desplazamiento del objeto anidado.")]
    public float displacementMagnitude = 0.5f;

    [Tooltip("La velocidad con la que el objeto anidado se mueve a su nueva posición.")]
    public float smoothSpeed = 5f;

    // Referencia al PlayerInput para obtener la dirección de movimiento.
    // Podrías pasársela desde tu CharacterController si este script no está en el mismo GameObject.
    private PlayerInput playerInput;
    private InputAction moveAction;

    // Para almacenar la dirección de entrada del jugador
    private Vector2 currentInputDirection;
    private Vector2 previousInputDirection;

    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>(); // Busca el PlayerInput en los padres si no está en el mismo GO
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>(); // Intenta en el mismo GO
        }

        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"]; // Asume que tienes una acción "Move"
        }
        else
        {
            Debug.LogError("PlayerInput no encontrado. Asegúrate de que el jugador tenga el componente PlayerInput.");
        }

        if (childToMove == null)
        {
            Debug.LogError("No hay un GameObject hijo asignado a 'childToMove' en el Inspector.");
        }
    }

    void Update()
    {
        if (moveAction != null)
        {
            // Lee la dirección de movimiento del Input System
            currentInputDirection = moveAction.ReadValue<Vector2>();
        }


        if (currentInputDirection.magnitude > 0.01f) // Evitar normalizar un vector casi cero
        {
            currentInputDirection.Normalize();
        }
        if (currentInputDirection != previousInputDirection)
        {
            // Si la dirección ha cambiado, actualiza la posición del hijo
            UpdateChildPositionInstant();
        }
        
    }

    private void UpdateChildPositionInstant()
    {
        // Calcula la posición objetivo local para el GameObject anidado
        // La dirección de input es 2D (X, Y). La convertimos a 3D (X, Z) para el movimiento en el suelo.
        Vector3 targetLocalPosition = new Vector3(
            currentInputDirection.x * displacementMagnitude,
            0f, // No movemos en Y localmente a menos que sea un juego 3D con movimiento vertical por input
            currentInputDirection.y * displacementMagnitude
        );

        // Asigna la posición de forma INSTANTÁNEA (sin Lerp)
        childToMove.localPosition = targetLocalPosition;

        Debug.Log($"Posición del hijo actualizada a: {targetLocalPosition} (Input: {currentInputDirection})");
    }
}
