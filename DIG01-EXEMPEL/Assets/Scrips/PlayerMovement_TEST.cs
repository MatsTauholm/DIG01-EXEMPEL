using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_TEST : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 850;
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
       // groundMask = 1 << LayerMask.NameToLayer("Ground");

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
        //RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, castDistance, ground); //See if ray hits ground below players position
        if(Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.85f,0.05f), CapsuleDirection2D.Horizontal, 0 ,ground))
        {
            isGrounded = true;
            Debug.Log("OnGround");
        }
        else
        {
            isGrounded = false;
        }
        //if (hit.collider != null)
       
        //Check update movement based on input and assigne
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        //Check if moving and not attacking or falling
        if (isGrounded && !isAttacking)
        {
            if (moveInput.x != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
                transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);  //Mirror sprite if moving left
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        //Check if trying to jump and on ground
        if (isJumpPressed && isGrounded)
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

                if (isGrounded) //Check which animation should be played
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
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangeAnimationState(PLAYER_DAMAGE);
        }
    }
    */
    //Animation manager
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}
