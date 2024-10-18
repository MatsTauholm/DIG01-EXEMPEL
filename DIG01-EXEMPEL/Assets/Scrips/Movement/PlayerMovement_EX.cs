using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_EX : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    int currentMovement = 1;
    Vector2 moveInput;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (currentMovement == 2) //AddForce movement
        { rb.AddForce(moveInput * moveSpeed); }

        if (currentMovement == 3) //MovePosition movement
        { rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.deltaTime)); }

    }

    void Update()
    {

        if (currentMovement == 1) //Transform movement
        { transform.Translate(moveInput * moveSpeed * Time.deltaTime); }

        if (currentMovement == 4) //Velocity movement
        { rb.velocity = moveInput * moveSpeed; }


        for (int i = 0; i <= 9; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                ChangeMovement(i);
            }
        }
    }

    void ChangeMovement(int newNumber)
    {
        currentMovement = newNumber;
        Debug.Log(currentMovement);
    }
}
