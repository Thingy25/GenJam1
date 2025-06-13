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



    // --- NUEVO: Evento para notificar el inicio del deslizamiento ---
    public event Action OnSlideStarted;
    // --- NUEVO: Evento para notificar el fin del deslizamiento ---
    public event Action OnSlideStopped;

    // Variables internas para controlar el estado del deslizamiento
    private bool wasSlidingLastFrame = false; // Para saber si estaba deslizándose en el frame anterior

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
        // Asegúrate de detener el sonido de deslizamiento si el script se deshabilita
        if (wasSlidingLastFrame)
        {
            OnSlideStopped?.Invoke();
            wasSlidingLastFrame = false;
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

    // También necesitarás OnCollisionExit para saber cuándo deja de estar en el suelo
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    protected override void Update()
    {
        base.Update(); // Llama al Update de la clase base para el movimiento horizontal

        // --- NUEVO: Lógica para disparar eventos de deslizamiento ---
        bool isMovingHorizontally = Mathf.Abs(inputDirection.x) > 0.05f; // Verifica si hay input horizontal significativo
        bool isCurrentlySliding = isGrounded && isMovingHorizontally;

        if (isCurrentlySliding && !wasSlidingLastFrame)
        {
            // Acaba de empezar a deslizarse
            OnSlideStarted?.Invoke();
   
        }
        else if (!isCurrentlySliding && wasSlidingLastFrame)
        {
            // Acaba de dejar de deslizarse
            OnSlideStopped?.Invoke();
   
        }

        wasSlidingLastFrame = isCurrentlySliding;
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

        // Si estaba deslizándose, detén el sonido de deslizamiento
        if (wasSlidingLastFrame)
        {
            OnSlideStopped?.Invoke();
            wasSlidingLastFrame = false;
        }
    



}

    private void CancelJump(InputAction.CallbackContext context)
    {
        playerRb.AddForce(Vector2.up * -jumpForce * smoothFallFactor, ForceMode.Impulse);
    }
}