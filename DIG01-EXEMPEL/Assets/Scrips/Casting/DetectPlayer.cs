using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] float circleRadius = 0.5f;
    [SerializeField] float castDistance = 5f;
    [SerializeField] LayerMask detectionLayer;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckBelow();
    }

    void CheckBelow()
    {
        // Perform a CircleCast downwards to check for the player (see the inspector for the detectionLayer to make sure it includes the player's layer)
        RaycastHit2D hit = Physics2D.CircleCast( 
            transform.position,
            circleRadius,
            Vector2.down,
            castDistance,
            detectionLayer);
        {
            if (hit == true)
            {
                Fall();
                Debug.Log("Player is below this object!");
            }
        }
    }

    private void Fall()
    {
        rb.WakeUp(); // Make the Rigidbody2D active and  respond to physics (start falling)
        spriteRenderer.color = Color.red; // Change color to red to indicate the object is falling
    }

    //Visualize the cast in Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius); //Start point of the cast
        Gizmos.DrawWireSphere(transform.position + Vector3.down * castDistance, circleRadius); //End point of the cast
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * castDistance); //Line representing the cast
    }
}