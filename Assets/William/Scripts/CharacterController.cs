using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    // Variables
    private CharacterManager characterManagerScript;

    protected PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction switchCharacter;

    protected Rigidbody playerRb;

    private Vector2 inputDirection;
    [SerializeField] private float speed;

    void Awake()
    {
        characterManagerScript = GetComponent<CharacterManager>();

        // Get the actions through InputSystem
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];
        switchCharacter = playerInput.actions["SwitchCharacter"];

        playerRb = GetComponent<Rigidbody>();
    }

    // Enable Input only when object is enabled in scene
    void OnEnable()
    {
        switchCharacter.started -= SwitchCharacter; // Asegura no duplicar
        switchCharacter.started += SwitchCharacter;
    }

    // Disable Input if the object is no longer enabled in the scene
    void OnDisable()
    {
        if (switchCharacter != null)
        {
            switchCharacter.started -= SwitchCharacter;
        }
    }

    protected virtual void Update()
    {
        inputDirection = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputDirection.x  * speed, playerRb.linearVelocity.y, inputDirection.y  * speed);
        playerRb.linearVelocity = movement;
    }

    // 
    private void SwitchCharacter(InputAction.CallbackContext context)
    {
        // Notifica al CharacterManager para que haga el swap
        if (characterManagerScript != null)
        {
            characterManagerScript.ToggleCharacter();
        }
    } 
}
