using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementGamepad : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 aimInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        //Movement
        rb.linearVelocity = moveInput * moveSpeed;

        //Rotation (Aim)
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime / 360f);
            rb.MoveRotation(angle);
        }
    }
}

