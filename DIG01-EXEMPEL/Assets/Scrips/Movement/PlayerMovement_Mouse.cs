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
    int currentMovement = 1;
    Vector2 target;
    Vector2 velocity = Vector2.zero;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseMove()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        // Convert the screen position to a world position
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
    }

    void Update()
    {
        if (currentMovement == 1)
        { transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime); }

        if (currentMovement == 2)
        { transform.position = Vector2.SmoothDamp(transform.position, target, ref velocity, moveTime * Time.deltaTime); }


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
