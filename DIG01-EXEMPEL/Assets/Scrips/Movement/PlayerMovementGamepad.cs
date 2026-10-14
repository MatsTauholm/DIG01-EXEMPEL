using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementGamepad : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private bool smoothRotation;

    [SerializeField] private Transform aimRay;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 aimInput;
    private float attackAmount;
    private Gamepad gamepad;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gamepad = Gamepad.current;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Move();
        Attack();
    }

    private void Attack()
    {
        attackAmount = Gamepad.current.rightTrigger.ReadValue();
        aimRay.localScale = new Vector2(attackAmount * .5f, aimRay.transform.localScale.y);
    }   

    private void Move()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void FixedUpdate()
    {
        Aim(); 
    }

    private void Aim() //Rotation (Aim)
    {
        if (aimInput.magnitude < 0.2f) //Stop the player from rotating back to origin when the right stick is not being used
            return;

        float targetAngle = Vector2.SignedAngle(Vector2.up, aimInput); // Calculate the target angle based on the aim input OBS! If object is rotated to the right, change Vector2.up to Vector2.right

        if (smoothRotation)
        {
            float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime / 360f);  //Lerp the angle for smooth rotation
            rb.MoveRotation(smoothAngle);
        }
        else
        {
            rb.MoveRotation(targetAngle);
        }
    }
}

