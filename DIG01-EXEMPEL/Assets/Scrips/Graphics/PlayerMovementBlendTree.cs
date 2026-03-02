using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBlendTree : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
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

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }
}
