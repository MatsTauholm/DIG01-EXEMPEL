using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Animation : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 850;
    [SerializeField] private float attackDelay = 0.3f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    public ContactFilter2D groundFilter;

    private bool isJumpPressed;
    private bool isGrounded;
    private bool isAttackPressed;
    private bool isAttacking;
    private int groundMask;
    private string currentAnimaton;

    //Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_AIR_ATTACK = "Player_Air_Attack";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void OnJump()
    {
        isJumpPressed = true;
    }

    void OnAttack()
    {
        isAttackPressed = true;
    }

    private void Update()
    {
        //Check if moveing and not attacking or falling
        if (isGrounded && !isAttacking)
        {
            if (movement.x != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
                transform.localScale = new Vector2(Mathf.Sign(movement.x), transform.localScale.y);  //Mirror sprite if moving left
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
    }

    private void FixedUpdate()
    {
        //Check if player is on the ground (Works)
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask); //See if ray hits ground below players position
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }*/

        //Check if player is on the ground (Don't work)
        isGrounded = rb.IsTouching(groundFilter);

        //Check update movement based on input and assigne
        Vector2 vel = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        rb.velocity = vel;

        //Check if trying to jump 
        if (isJumpPressed && isGrounded)
        {    
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
            isGrounded = false;
        }

        //Attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking) //Check if player is currently attacking 
            {
                isAttacking = true;

                if (isGrounded)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                }
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                }
                Invoke("AttackComplete", attackDelay); //Run method after delay
            }
        }
    }

    //Reset bool after attackdelay
    void AttackComplete()
    {
        isAttacking = false;
    }

    //Animation manager
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return; 

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}
