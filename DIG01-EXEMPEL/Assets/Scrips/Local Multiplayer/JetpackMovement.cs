using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jetpackForce = 5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFly(InputValue button)
    {
        Debug.Log("Button pressed");
        if (button.isPressed)
        {
            StartFlying();
        }
        else
        {
            StopFlying();
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Player Movement
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;
    }

    private void StartFlying()
    {
        rb.AddForce(Vector2.up * jetpackForce);
    }

    private void StopFlying()
    {

    }
}
