using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_TEST : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 850;
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float castDistance;

    [SerializeField] ContactFilter2D groundFilter;

    Animator animator;
    Rigidbody2D rb;
    Collider2D coll;

    Vector2 moveInput;

    bool isJumpPressed;
    bool isGrounded;
    bool isAttackPressed;
    bool isAttacking;
    int groundMask;
    string currentAnimaton;

    //Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_DAMAGE = "Player_Damage";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_AIR_ATTACK = "Player_Air_Attack";

    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Attacking,
    }
    PlayerState currentState = PlayerState.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        isJumpPressed = true;
    }

    void OnAttack()
    {
        isAttackPressed = true;
    }

    private void FixedUpdate()
    {
        //Check if player is on the ground    
        if(rb.IsTouching(groundFilter))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.Log(isGrounded);
        //Check update movement based on input and assigne
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        //Check if moving and not attacking or falling
        if (isGrounded && !isAttacking)
        {
            if (moveInput.x != 0)
            {
                
                transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);  //Mirror sprite if moving left
            }
            else
            {
              //  animator.CrossFadeInFixedTime(PLAYER_IDLE, 0.2f);
            }
        }

        //Check if trying to jump and on ground
        if (isJumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            //animator.CrossFadeInFixedTime(PLAYER_JUMP, 0.2f);
        }

        //Attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking) //Check if player is currently attacking 
            {
                isAttacking = true;

                if (isGrounded) //Check which animation should be played
                {
                    //animator.CrossFadeInFixedTime(PLAYER_ATTACK, 0.2f);
                }
                else
                {
                   // animator.CrossFadeInFixedTime(PLAYER_AIR_ATTACK, 0.2f);
                }
                Invoke("AttackComplete", attackDelay); //Run method after delay
            }
        }
        ChangeAnimation();
    }

    //Reset bool after attackdelay
    void AttackComplete()
    {
        isAttacking = false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
          //  animator.CrossFadeInFixedTime(PLAYER_DAMAGE, 0.2f);
        }
    }  
    
    void ChangeAnimation()
    {
        switch(currentState)
        {
            case PlayerState.Attacking:
                if(isGrounded == true)
                {
                    animator.Play(PLAYER_ATTACK);
                }
                else
                {
                    animator.Play(PLAYER_AIR_ATTACK);
                }
            break;

            case PlayerState.Jumping:
                {

                }
                break;
            case PlayerState.Running:
                if (moveInput == Vector2.zero)
                {
                    animator.CrossFadeInFixedTime(PLAYER_IDLE, 0f);
                    currentState = PlayerState.Idle;
                }
                break;
            case PlayerState.Idle:
                if (moveInput != Vector2.zero)
                {
                    animator.CrossFadeInFixedTime(PLAYER_RUN, 0f);
                    currentState = PlayerState.Running;
                }
                break;
        }
    }

}
