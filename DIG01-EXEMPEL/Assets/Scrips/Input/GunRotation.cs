using UnityEngine;
using UnityEngine.InputSystem;

public class GunRotation : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        RotateTowardMouse();
    }

    private void RotateTowardMouse()
    {
        //Get the current mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Calculate the direction from the object to the mouse
        Vector2 direction = mousePosition - rb.position;

        // Calculate the angle from the front of the gun (right) to the direction. (OBS! - If your object is facing up, you might want to use Vector2.up instead of Vector2.right)
        float angle = Vector2.SignedAngle(Vector2.right, direction);

        // Set the rotation of the object to face the mouse
        rb.rotation = angle;
    }
}
