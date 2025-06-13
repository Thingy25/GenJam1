using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterTwoController : CharacterController
{
    // Variables
    private InputAction flyAction;

    [SerializeField] private bool isGrounded = false;
    private bool isFlying = false;
    [SerializeField] private float flightForce;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRecoveryRate;
    private float currentEnergy;

    // Enable Input System Actions only when object is enabled in scene
    void OnEnable()
    {
        currentEnergy = maxEnergy;

        // Get the actions through InputSystem
        flyAction ??= playerInput.actions["Fly"];

        flyAction.performed -= StartFly;
        flyAction.performed += StartFly;

        flyAction.canceled -= StopFly;
        flyAction.canceled += StopFly;
    }
    void OnDisable()
    {
        if (flyAction != null)
        {
            flyAction.performed -= StartFly;
            flyAction.canceled -= StopFly;
        }
    }

    protected override void Update()
    {
        base.Update();
        // Recargar stamina si está en el suelo y no volando
        if (isGrounded && currentEnergy < maxEnergy)
        {
            currentEnergy += energyRecoveryRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
        } 
    }

    void FixedUpdate() {
        // Solo vuela si se está presionando el botón y queda stamina
        if (isFlying && currentEnergy > 0f)
        {
            playerRb.AddForce(Vector3.up * flightForce, ForceMode.Acceleration);
            currentEnergy -= Time.deltaTime;
            Debug.Log(currentEnergy);
        }        
    }

    void OnCollisionStay(Collision collision)
    {
        // Can only fly if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void StartFly(InputAction.CallbackContext callback)
    {
        if (!isGrounded) return;

        //playerRb.AddForce(Vector2.up * flightForce, ForceMode.Impulse);
        isFlying = true;
        isGrounded = false;
    }

    private void StopFly(InputAction.CallbackContext callback)
    {
        playerRb.AddForce(Vector3.up * -flightForce, ForceMode.Acceleration);

        isFlying = false;
    }
}