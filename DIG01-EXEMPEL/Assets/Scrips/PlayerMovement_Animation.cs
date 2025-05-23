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
    //bool isGrounded;
    private bool isJumpPressed;
    private bool isAttackPressed;
    private bool isAttacking;
    private string currentAnimaton;

    Collider2D bodyColl;
    Collider2D feetColl;

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
        bodyColl = GetComponent<CapsuleCollider2D>();
        feetColl = GetComponent<BoxCollider2D>();
    }

    public bool isGrounded()
    {
        if (feetColl.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return true;
        }
        else
            { return false; }
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
        if (isGrounded() && !isAttacking)
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
    private void LateUpdate()
    {
    
        //Check update movement based on input and assigne
        Vector2 vel = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        rb.velocity = vel;

        //Check if trying to jump 
        if (isJumpPressed && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }

        //Attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking) //Check if player is currently attacking 
            {
                isAttacking = true;

                if (isGrounded())
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
