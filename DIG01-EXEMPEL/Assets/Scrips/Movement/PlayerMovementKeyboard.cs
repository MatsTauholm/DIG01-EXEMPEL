using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private MovementSystemManager movementSystemManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSystemManager = FindFirstObjectByType<MovementSystemManager>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (movementSystemManager.currentMovement == 2) //AddForce movement
        { 
            rb.AddForce(moveInput * moveSpeed);
        }

        if (movementSystemManager.currentMovement == 3) //MovePosition movement
        { 
            rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.deltaTime));
        }

    }

    void Update()
    {
        if (movementSystemManager.currentMovement == 1) //Transform movement
        { 
            transform.Translate(moveInput * moveSpeed * Time.deltaTime);
        }

        if (movementSystemManager.currentMovement == 4) //Velocity movement
        { 
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}
