using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterOneController : CharacterController
{
    // Variables
    private InputAction jumpAction;

    [SerializeField]private bool isGrounded = false;
    [SerializeField] private float jumpForce;
    [SerializeField] private float smoothFallFactor;

    public Sprite characterOneSprite;
    // private Vector3 characterOneColliderCenter = new Vector3(0.15f, 3.6f, 0);
    // private Vector3 characterOneColliderSize = new Vector3(4, 7.3f, 0.2f);

    // Enable Input System Actions only when object is enabled in scene
    void OnEnable()
    {
        // characterCollider.center = new Vector3(0.15f, 3.6f, 0);
        // characterCollider.size = new Vector3(4, 7.3f, 0.2f);

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
            characterAnimator.SetBool("b_isJumping", false);
        }
    }

    // The height of the jump depends on how far the button is pressed
    private void StartJump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        playerRb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        characterAnimator.SetBool("b_isJumping", true);
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        playerRb.AddForce(Vector2.up * -jumpForce * smoothFallFactor, ForceMode.Impulse);
    }
}
