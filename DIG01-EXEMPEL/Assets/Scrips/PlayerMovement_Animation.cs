using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Animation : MonoBehaviour
{
    public ContactFilter2D groundFilter;
    private Vector2 movement;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator ani;
    private int groundMask;

    private float xAxis;
    public float moveSpeed = 5f;
    public float jumpImpulse = 5f;
    private bool isGrounded;
    private bool isJumpPressed;
    private string currentState;

    //Animation states satta som konstanter för att slippa använda strings senare i koden
    const string PLAYER_IDLE = "HeroKnight_Idle";
    const string PLAYER_RUN = "HeroKnight_Run";
    //const string PLAYER_ATTACK = "HeroKnight_Attack1";
    //const string PLAYER_DEAD = "HeroKnight_Death";
    const string PLAYER_JUMP = "HeroKnight_Jump";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        //Checking for inputs
        xAxis = Input.GetAxisRaw("Horizontal");

        //space jump key pressed?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //isGrounded = rb.IsTouching(groundFilter); //Om spelaren rör vid marken

        Vector2 playerVelocity = new Vector2(xAxis * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;


        if (isGrounded)
        {
            if (xAxis == 0) //Om inte spelaren rör på sig och står på marken
            {
                ChangeAnimationState(PLAYER_IDLE); //Ändra state till "Idle"
            }
            else
            {
                ChangeAnimationState(PLAYER_RUN);
                transform.localScale = new Vector2(Mathf.Sign(xAxis), transform.localScale.y);  //Spegla sprite:n om spelaren rör sig åt vänste
            }
        }

        if (isJumpPressed && isGrounded)  //Om spelaren är på marken och spelaren har tryckt på hoppknappen
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }

        Debug.Log(currentState);
        Debug.Log(isGrounded);
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
