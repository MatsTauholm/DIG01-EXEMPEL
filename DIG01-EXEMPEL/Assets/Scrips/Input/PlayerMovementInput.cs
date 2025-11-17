using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovementInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject bullet;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private bool isFiring;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isFiring = true;
        }
        else if (ctx.canceled)
        {
            isFiring = false;
        }
    }

    public void OnHold (InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Holding!");
        }
    }

    public void OnDoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Double Tapped!");
        }
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        Shoot();
    }

    private void Shoot()
    {
        if (isFiring)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
}

