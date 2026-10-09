using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{  
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            target.position,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);

        //transform.position = Vector2.MoveTowards(
        //    rb.position,
        //    target.position,
        //    speed * Time.fixedDeltaTime
        //);



        // Check if we reached the target
        if (Vector2.Distance(rb.position, target.position) < 0.01f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}