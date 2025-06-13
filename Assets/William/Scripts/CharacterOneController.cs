using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterOneController : CharacterController
{
    // Variables
    private InputAction jumpAction;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float jumpForce;
    [SerializeField] private float smoothFallFactor;

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

    void OnCollisionStay(Collision collision)
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
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        playerRb.AddForce(Vector2.up * -jumpForce * smoothFallFactor, ForceMode.Impulse);
    }
}
