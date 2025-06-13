using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    // Variables
    private CharacterManager characterManagerScript;
    protected SpriteRenderer characterSpriteRenderer;

    protected PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction switchCharacter;

    protected Rigidbody playerRb;
    protected BoxCollider characterCollider;

    private Vector2 inputDirection;
    [SerializeField] private float speed;

    protected Animator characterAnimator;

    void Awake()
    {
        characterManagerScript = GetComponent<CharacterManager>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();

        // Get the actions through InputSystem
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];
        switchCharacter = playerInput.actions["SwitchCharacter"];

        playerRb = GetComponent<Rigidbody>();

        characterAnimator = GetComponent<Animator>();
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

    protected virtual void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputDirection.x * speed, playerRb.linearVelocity.y, inputDirection.y * speed);
        playerRb.linearVelocity = movement;

        characterAnimator.SetFloat("f_ySpeed", playerRb.linearVelocity.y);


        //characterAnimator.SetFloat("f_zSpeed", Math.Abs(playerRb.linearVelocity.z));

        if (inputDirection.y > 0)
        {
            characterAnimator.SetBool("b_isFrontOriented", false);
        }
        else if (inputDirection.y < 0)
        {
            characterAnimator.SetBool("b_isFrontOriented", true);
        }

        if (inputDirection.x > 0)
        {
            characterAnimator.SetBool("b_isWalking", true);
            characterSpriteRenderer.flipX = false;
        }
        else if (inputDirection.x < 0)
        {
            characterAnimator.SetBool("b_isWalking", true);
            characterSpriteRenderer.flipX = true;
        }
        else
        {
            characterAnimator.SetBool("b_isWalking", false);
        }

        UpdateAnimatorSet();
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
    
    private void UpdateAnimatorSet()
    {
        int set = 0;

        bool isGhost = characterManagerScript.selectedCharacter == CharacterManager.CharacterType.two;
        bool isFront = characterAnimator.GetBool("b_isFrontOriented");

        if (!isGhost && isFront) set = 0;
        else if (!isGhost && !isFront) set = 1;
        else if (isGhost && isFront) set = 2;
        else if (isGhost && !isFront) set = 3;

        characterAnimator.SetInteger("i_currentState", set);
    }
}
