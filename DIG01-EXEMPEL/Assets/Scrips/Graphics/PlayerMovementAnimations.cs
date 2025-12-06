using System;
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
    private bool isDead;
    private Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        if (!isDead)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump()
    {
        if (isGrounded && !isDead)
        {
            shouldJump = true;
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        
        //Running Animation
        if (moveInput.x != 0)
        {
            ani.SetBool("isRunning", true);
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y); //Flip sprite if moving left
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
        ani.SetBool("isGrounded", isGrounded);

        //Jump Animation
        if (!isGrounded)
        {
            ani.SetTrigger("Jumping");
        }

        Jump();           
    }

    private void Jump()
    {
        //Player Jump
        if (shouldJump)
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            shouldJump = false;
        }  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coll.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isDead = true;
            ani.SetBool("isDead", true);
            //coll.enabled = false;
            //rb.linearVelocity = new Vector2(20 * transform.localScale.x, 20);
        }
    }

}
