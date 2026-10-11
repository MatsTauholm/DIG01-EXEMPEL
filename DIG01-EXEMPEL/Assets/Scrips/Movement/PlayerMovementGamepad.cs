using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementGamepad : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    [SerializeField] private Transform aimRay;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 aimInput;
    private float attackAmount;

    private void Start()
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
        float targetAngle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg - 90f;
        float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime / 360f);
        rb.MoveRotation(angle);
    }

}

