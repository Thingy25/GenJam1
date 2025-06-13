using UnityEngine;
using UnityEngine.InputSystem;
using System; // Necesario para 'Action'

public class CharacterOneControllerWilson : CharacterControllerWilson
{
    // Variables
    private InputAction jumpAction;

    private bool isGrounded = false;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float smoothFallFactor = 0.5f;

    // --- NUEVO: Evento para notificar cuando el personaje salta ---
    public event Action OnJumpStarted;

    // Enable Input System Actions only when object is enabled in scene
    void OnEnable()
    {
        // Get the actions through InputSystem
        jumpAction ??= playerInput.actions["Jump"];

        jumpAction.started -= StartJump; // Cleaning
        jumpAction.started += StartJump;

        jumpAction.canceled -= CancelJump;
        jumpAction.canceled += CancelJump;
    }

    // Disable Input System Actions if the object is no longer enabled in the scene
    void OnDisable()
    {
        if (jumpAction != null)
        {
            jumpAction.started -= StartJump;
            jumpAction.canceled -= CancelJump;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Can only jump if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // The height of the jump depends on how far the button is pressed
    private void StartJump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        playerRb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

        Debug.Log("Evento OnJumpStarted invocado!");

        // --- NUEVO: Invoca el evento cuando el salto comienza ---
        OnJumpStarted?.Invoke(); // Esto notifica a todos los suscriptores

       
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        playerRb.AddForce(Vector2.up * -jumpForce * smoothFallFactor, ForceMode.Impulse);
    }
}