using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Vector2 moveInput;

    void OnMove(InputValue value)
    {
         moveInput = value.Get<Vector2>();   
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, 0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}