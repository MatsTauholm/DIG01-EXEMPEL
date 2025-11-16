using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnClickStarted(InputAction.CallbackContext ctx)
    {
        Debug.Log("Button pressed");
            isFiring = true;
        
    }

    void OnClickCanceled(InputAction.CallbackContext ctx)
    {
        Debug.Log("Button released");
        isFiring = false;
    }
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        Shoot();
        Debug.Log(isFiring);
    }

    private void Shoot()
    {
        if (isFiring)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
}

