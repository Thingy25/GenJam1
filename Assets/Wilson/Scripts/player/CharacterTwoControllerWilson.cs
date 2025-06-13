using UnityEngine;
using UnityEngine.InputSystem;
using System; // Necesario para 'Action'

public class CharacterTwoControllerWilson : CharacterControllerWilson
{
    // Variables
    private InputAction flyAction;

    private bool isGrounded = false;
    private bool isFlying = false;
    [SerializeField] private float flightForce = 5;
    [SerializeField] private float maxStamina = 3;

    // --- NUEVO: Evento para notificar cuando el personaje empieza a volar ---
    public event Action OnFlyStarted;

    // Enable Input System Actions only when object is enabled in scene
    void OnEnable()
    {
        // Get the actions through InputSystem
        flyAction ??= playerInput.actions["Fly"];

        flyAction.performed -= StartFly;
        flyAction.performed += StartFly;
    }
    void OnDisable()
    {
        if (flyAction != null)
        {
            flyAction.performed -= StartFly;
        }
    }

    private void StartFly(InputAction.CallbackContext callback)
    {
        if (!isGrounded) return;

        playerRb.AddForce(Vector2.up * flightForce, ForceMode.Impulse);
        isGrounded = false;
        Debug.Log("Evento OnFlyStarted invocado!");
        // --- NUEVO: Invoca el evento cuando el vuelo comienza ---
        OnFlyStarted?.Invoke(); // Notifica a los suscriptores
       
    }

    private void StopFly()
    {
        // Tu lógica para detener el vuelo, si la tienes
    }
}