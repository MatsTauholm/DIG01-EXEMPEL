using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBlendTree : MonoBehaviour
{
    public float moveSpeed = 5f;
    public AnimatorOverrideController overrideController; //AnimatorOverrideController är en variant av animatorn som låter oss byta ut animationerna, utan att ändra på själva animatorn

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator ani;
    private RuntimeAnimatorController originalController; //RuntimeAnimatorController är en variant av animatorn som är optimiserad för runtime (dvs. medan spelet körs) 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        originalController = ani.runtimeAnimatorController; //Vi sparar den ursprungliga animatorn så att vi kan byta tillbaka till den senare

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //Set animator parameter if input is given
        if (moveInput != Vector2.zero)
        {
            ani.SetFloat("XInput", moveInput.x);
            ani.SetFloat("YInput", moveInput.y);
        }
    }

    private void OnTransform()
    {
        if (ani != null)
        {
            SwitchAnimationController();
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    private void SwitchAnimationController()
    {
        if (ani.runtimeAnimatorController == originalController)
        {
            ani.runtimeAnimatorController = overrideController; //Byt till override controller
        }
        else
        {
            ani.runtimeAnimatorController = originalController; //Byt till original controller
        }
    }
}
