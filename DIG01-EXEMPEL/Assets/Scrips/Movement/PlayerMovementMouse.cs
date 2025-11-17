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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSystemManager = FindFirstObjectByType<MovementSystemManager>();
    }

    void OnClick(InputValue button)
    {
        Debug.Log("Mouse Clicked");
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
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        //Moving with SmoothDamp
        if (movementSystemManager.currentMovement == 2)
        {
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, moveTime * Time.deltaTime);
        }
        
    }

    private void PlayerRotate()
    {
        //Get the current mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the object to the mouse
        Vector2 direction = mousePosition - rb.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the object to face the mouse
        rb.rotation = angle;
    }
}
