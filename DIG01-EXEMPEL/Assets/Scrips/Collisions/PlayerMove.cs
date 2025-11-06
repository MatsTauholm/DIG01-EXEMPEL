using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;

    private Vector2 mousePosition;
    private Vector2 targetPosition;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnClick(InputValue button)
    {
        targetPosition = mousePosition;
    }

    void Update()
    {
        PlayerRotate();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, moveTime * Time.deltaTime);
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

