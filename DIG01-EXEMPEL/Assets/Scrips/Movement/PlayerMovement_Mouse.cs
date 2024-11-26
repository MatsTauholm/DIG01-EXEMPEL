using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement_Mouse : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveTime;
    [SerializeField] GameObject infoText;

    int currentMovement = 1;
    Vector2 mousePosition;
    Vector2 target;
    Vector2 velocity = Vector2.zero;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Get the current mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PlayerMove();
        PlayerRotate();

        //Checking whitch numberkey is pressed
        for (int i = 0; i <= 9; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                ChangeMovement(i); //Change the current way of movement
            }
        }
    }

    void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0)) //If left mousebutton is pressed
        {
            // Convert the screen position to a world position
            target = mousePosition;
        }

        //Moving with Move Towards
        if (currentMovement == 1)
        { 
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            infoText.GetComponent<ChangeInfoText>().UpdateText("Vector2 MoveTowards");
        }

        //Moving with SmoothDamp
        if (currentMovement == 2)
        {
            transform.position = Vector2.SmoothDamp(transform.position, target, ref velocity, moveTime * Time.deltaTime);
            infoText.GetComponent<ChangeInfoText>().UpdateText("Vector2 SmoothDamp");
        }
        
    }

    void PlayerRotate()
    {
        // Calculate the direction from the object to the mouse
        Vector2 direction = mousePosition - rb.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the object to face the mouse
        rb.rotation = angle;
    }

    void ChangeMovement(int newNumber)
    {
        currentMovement = newNumber;
        Debug.Log(currentMovement);
    }
}
