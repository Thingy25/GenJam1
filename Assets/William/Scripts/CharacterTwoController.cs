using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterTwoController : CharacterController
{
    // Variables
    private InputAction flyAction;

    private bool isGrounded = false;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float timeOfFlight = 3;

    // Enable Input System Actions only when object is enabled in scene
    void OnEnable()
    {
        // Get the actions through InputSystem
        flyAction ??= playerInput.actions["Fly"];

        flyAction.performed  -= StartFly;
        flyAction.performed  += StartFly;
    }
    void OnDisable()
    {
        if (flyAction != null)
        {
            flyAction.performed  -= StartFly;
        }
    }

    private void StartFly(InputAction.CallbackContext callback) {
        if (!isGrounded) return;

        playerRb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
}
