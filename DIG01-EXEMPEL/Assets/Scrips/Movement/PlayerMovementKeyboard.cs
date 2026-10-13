using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;

    private Vector2 moveInput;
    private Vector2 mousePosition;
    private Vector2 targetPosition;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;
    private MovementSystemManager movementSystemManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSystemManager = FindAnyObjectByType<MovementSystemManager>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnClick(InputValue button)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log("Mouse Clicked");
        targetPosition = mousePosition;
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
        
        if (movementSystemManager.currentMovement == 5) //Moving with Move Towards
        {
            rb.position = Vector2.MoveTowards(transform.position, targetPosition, (moveSpeed * 5) * Time.deltaTime);
        }

        if (movementSystemManager.currentMovement == 6) //Moving with SmoothDamp
        {
            rb.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, moveTime * Time.deltaTime);
        }
    }
}
