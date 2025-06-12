using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variables
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction switchCharacter;

    private Rigidbody playerRb;

    private Vector2 inputDirection;
    [SerializeField] private float speed;

    private bool isGrounded = false;
    [SerializeField] private float jumpForce;

    private bool isCharacterOneActive = true;

    public PlayerInput playerInput;

    void Awake()
    {
        // Get the actions through InputSystem
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        interactAction = playerInput.actions["Interact"];
        switchCharacter = playerInput.actions["SwitchCharacter"];

        playerRb = GetComponent<Rigidbody>();
    }

    // Enable Input System Actions only when object is enabled in scene
    void OnEnable()
    {
        jumpAction.started += StartJump;
        jumpAction.canceled += CancelJump;

        // switchCharacter.started += SwitchCharacter;
    }

    // Disable Input System Actions if the object is no longer enabled in the scene
    void OnDisable()
    {
        jumpAction.started -= StartJump;
        jumpAction.canceled -= CancelJump;

        // switchCharacter.started -= SwitchCharacter;
    }

    void Update()
    {
        inputDirection = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(inputDirection.x * speed, playerRb.linearVelocity.y);
        playerRb.linearVelocity = movement;
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
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        playerRb.AddForce(Vector2.up * -jumpForce, ForceMode.Impulse);
    }

    /*private void SwitchCharacter(InputAction.CallbackContext context)
    {
        isCharacterOneActive = !isCharacterOneActive;

        if (isCharacterOneActive)
        {
            playerInput.SwitchCurrentActionMap("CharacterOne");
            Debug.Log("1");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("CharacterTwo");
            Debug.Log("2");
        }
    }*/
}
