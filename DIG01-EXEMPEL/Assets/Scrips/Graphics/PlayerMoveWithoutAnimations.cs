using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveWithoutAnimations : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpImpulse = 5f;
    public ContactFilter2D groundFilter;
    public static bool isGrounded;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private bool shouldJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            shouldJump = true;
        }
    }

    void Update()
    {
        if (moveInput.x != 0)
        {
            //Flip sprite if moving left
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        //Groundcheck
        isGrounded = rb.IsTouching(groundFilter);

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
            FindFirstObjectByType<GameSession>().PlayerProcessDeath();
        }
    }

}

