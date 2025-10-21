using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Input References")]
    public InputActionReference moveAction; // Reference to Move action from PlayerInput


    [SerializeField] private GameObject infoText;
    
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private MovementSystemManager movementSystemManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementSystemManager = FindFirstObjectByType<MovementSystemManager>();
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }


    //void OnMove(InputValue value)
    //{
    //    moveInput = value.Get<Vector2>();
    //}

    void FixedUpdate()
    {
        if (moveAction != null)
            moveInput = moveAction.action.ReadValue<Vector2>();

        if (movementSystemManager.currentMovement == 2) //AddForce movement
        { 
            rb.AddForce(moveInput * moveSpeed);
            infoText.GetComponent<ChangeInfoText>().UpdateText("Rigidbody AddForce");
        }

        if (movementSystemManager.currentMovement == 3) //MovePosition movement
        { 
            rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.deltaTime));
            infoText.GetComponent<ChangeInfoText>().UpdateText("Rigidbody MovePosition");
        }

    }

    void Update()
    {
        if (movementSystemManager.currentMovement == 1) //Transform movement
        { 
            transform.Translate(moveInput * moveSpeed * Time.deltaTime);
            infoText.GetComponent<ChangeInfoText>().UpdateText("Transfrom Translate");
        }

        if (movementSystemManager.currentMovement == 4) //Velocity movement
        { 
            rb.linearVelocity = moveInput * moveSpeed;
            infoText.GetComponent<ChangeInfoText>().UpdateText("Rigidbody Velocity");
        }
    }
}
