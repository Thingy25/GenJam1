using UnityEngine;
using UnityEngine.InputSystem; 


public class PlayerMovementDependentChild : MonoBehaviour
{
    
    public Transform childToMove;

    [Tooltip("La fuerza/distancia del desplazamiento del objeto anidado.")]
    public float displacementMagnitude = 1.5f;

    [Tooltip("La velocidad con la que el objeto anidado se mueve a su nueva posici�n.")]
    public float smoothSpeed = 5f;

    // Referencia al PlayerInput para obtener la direcci�n de movimiento.
    // Podr�as pas�rsela desde tu CharacterController si este script no est� en el mismo GameObject.
    private PlayerInput playerInput;
    private InputAction moveAction;

    // Para almacenar la direcci�n de entrada del jugador
    private Vector2 currentInputDirection;
    private Vector2 previousInputDirection;

    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>(); // Busca el PlayerInput en los padres si no est� en el mismo GO
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>(); // Intenta en el mismo GO
        }

        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"]; // Asume que tienes una acci�n "Move"
        }
        else
        {
            Debug.LogError("PlayerInput no encontrado. Aseg�rate de que el jugador tenga el componente PlayerInput.");
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
            // Lee la direcci�n de movimiento del Input System
            currentInputDirection = moveAction.ReadValue<Vector2>();
        }


        if (currentInputDirection.magnitude > 0.01f) // Evitar normalizar un vector casi cero
        {
            currentInputDirection.Normalize();
        }
        if (currentInputDirection != previousInputDirection)
        {
            // Si la direcci�n ha cambiado, actualiza la posici�n del hijo
            UpdateChildPositionInstant();
        }
        
    }

    private void UpdateChildPositionInstant()
    {
        // Calcula la posici�n objetivo local para el GameObject anidado
        // La direcci�n de input es 2D (X, Y). La convertimos a 3D (X, Z) para el movimiento en el suelo.
        Vector3 targetLocalPosition = new Vector3(
            currentInputDirection.x * displacementMagnitude,
            0f, // No movemos en Y localmente a menos que sea un juego 3D con movimiento vertical por input
            currentInputDirection.y * displacementMagnitude
        );

        // Asigna la posici�n de forma INSTANT�NEA (sin Lerp)
        childToMove.localPosition = targetLocalPosition;

        Debug.Log($"Posici�n del hijo actualizada a: {targetLocalPosition} (Input: {currentInputDirection})");
    }
}
