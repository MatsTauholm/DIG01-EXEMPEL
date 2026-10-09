using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovementMouse : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;

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

    void OnClick(InputValue button)
    {
        targetPosition = mousePosition;
    }

    void Update()
    {
        PlayerRotate();
        PlayerMove();
    }

    private void PlayerMove()
    {        
        //Moving with Move Towards
        if (movementSystemManager.currentMovement == 1)
        { 
            rb.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        //Moving with SmoothDamp
        if (movementSystemManager.currentMovement == 2)
        {
            rb.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, moveTime * Time.deltaTime);
        }
        
    }

    private void PlayerRotate()
    {
        //Get the current mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Calculate the direction from the object to the mouse
        Vector2 direction = mousePosition - rb.position;

        // Calculate the angle from the front of the gun (right) to the direction. (OBS! - If your object is facing up, you might want to use Vector2.up instead of Vector2.right)
        float angle = Vector2.SignedAngle(Vector2.right, direction);

        // Set the rotation of the object to face the mouse
        rb.rotation = angle;
    }
}
