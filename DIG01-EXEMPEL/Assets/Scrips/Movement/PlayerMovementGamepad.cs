using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementGamepad : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Input References")]
    public InputActionReference moveAction; // Reference to Move action from PlayerInput
    public InputActionReference aimAction;  // Reference to Aim action from PlayerInput

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 aimInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (aimAction != null) aimAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (aimAction != null) aimAction.action.Disable();
    }

    private void FixedUpdate()
    {
        if (moveAction != null)
            moveInput = moveAction.action.ReadValue<Vector2>();
        if (aimAction != null)
            aimInput = aimAction.action.ReadValue<Vector2>();

        // --- Movement ---
        rb.linearVelocity = moveInput * moveSpeed;

        // --- Rotation (Aim) ---
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime / 360f);
            rb.MoveRotation(angle);
        }
    }
}

