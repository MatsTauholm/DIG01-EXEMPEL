using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Keyboard : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject infoText;
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
        { 
            rb.AddForce(moveInput * moveSpeed);
            infoText.GetComponent<ChangeInfoText>().UpdateText("Rigidbody AddForce");
        }

        if (currentMovement == 3) //MovePosition movement
        { 
            rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.deltaTime));
            infoText.GetComponent<ChangeInfoText>().UpdateText("Rigidbody MovePosition");
        }

    }

    void Update()
    {
        if (currentMovement == 1) //Transform movement
        { 
            transform.Translate(moveInput * moveSpeed * Time.deltaTime);
            infoText.GetComponent<ChangeInfoText>().UpdateText("Transfrom Translate");
        }

        if (currentMovement == 4) //Velocity movement
        { 
            rb.velocity = moveInput * moveSpeed;
            infoText.GetComponent<ChangeInfoText>().UpdateText("Rigidbody Velocity");
        }

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
