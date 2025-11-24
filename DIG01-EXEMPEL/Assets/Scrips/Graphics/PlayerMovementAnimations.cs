using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementAnimations : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpImpulse = 5f;

    public ContactFilter2D groundFilter;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    public static bool isGrounded; 
    private bool shouldJump;
    private Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        shouldJump = true;
    }

    void Update()
    {
        if (moveInput.x != 0)
        {
            ani.SetBool("isRunning", true);
            //Flip sprite if moving left
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }
        else
        {
            ani.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {
        //Groundcheck
        isGrounded = rb.IsTouching(groundFilter);

        //Jump Animation
        if (isGrounded)
        {
            ani.SetBool("isJumping", false);
        }
        else
        {
            ani.SetBool("isJumping", true);
        }

        //Player Movement
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;
       
        //Player Jump
        if (isGrounded && shouldJump)
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            shouldJump = false;
        }         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coll.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            ani.SetBool("isDead", true);
            FindFirstObjectByType<GameSession>().PlayerProcessDeath();
        }
    }

}
