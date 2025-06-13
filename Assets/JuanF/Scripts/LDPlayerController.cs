using UnityEngine;
using UnityEngine.InputSystem;

public class LDPlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1;

    PlayerInput playerInput;
    Rigidbody playerRb;
    Vector2 moveInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>().normalized;
        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime);
        //playerRb.MovePosition(transform.position + transform.TransformDirection(moveInput) * moveSpeed * Time.deltaTime);
    }
}
