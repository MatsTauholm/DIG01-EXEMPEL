using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Animation : MonoBehaviour
{
    

    public ContactFilter2D groundFilter;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator ani;

    public float moveSpeed = 5f;
    public float jumpImpulse = 5f;
    private bool isGrounded;
    private bool shouldJump;
    private string currentState;

    //Animation states
    const string PLAYER_IDLE = "HeroKnight_Idle";
    const string PLAYER_RUN = "HeroKnight_Run";
    const string PLAYER_ATTACK = "HeroKnight_Attack1";
    const string PLAYER_DEAD = "HeroKnight_Death";
    const string PLAYER_JUMP = "HeroKnight_Jump";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if(isGrounded)
        ChangeAnimationState(PLAYER_RUN);
    }

    void OnJump()
    {
        if (isGrounded)
        shouldJump = true;
    }

    void Update()
    {
        isGrounded = rb.IsTouching(groundFilter);  
        //Om inte spelaren rör på sig och står på marken
        if (moveInput.Equals(Vector2.zero) && isGrounded)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
        Debug.Log(currentState);
        Debug.Log(isGrounded);

    }

    void FixedUpdate()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (isGrounded && shouldJump)
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            shouldJump = false;
            ChangeAnimationState(PLAYER_JUMP);
        }         
    }
    void ChangeAnimationState(string newState) 
    {
        //Stoppa samma animation från att avbryta sig själv
        if (currentState == newState) return;

        //Spela animationen
        ani.Play(newState);

        //Uppdatera den nuvarande animationen
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coll.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            FindObjectOfType<GameSession>().PlayerProcessDeath();
        }
    }

}
