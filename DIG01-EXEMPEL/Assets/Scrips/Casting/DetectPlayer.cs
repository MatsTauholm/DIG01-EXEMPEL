using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] float circleRadius = 0.5f;
    [SerializeField] float castDistance = 5f;
    [SerializeField] LayerMask detectionLayer;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckBelow();
    }

    void CheckBelow()
    {
        RaycastHit2D hit = Physics2D.CircleCast(
            transform.position,
            circleRadius,
            Vector2.down,
            castDistance,
            detectionLayer);
        {
            if (hit == true && hit.collider.CompareTag("Player"))
            {
                Fall();
                Debug.Log("Player is below this object!");
            }
        }
    }

    private void Fall()
    {
        rb.WakeUp();
    }

    //Visualize the cast in Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
        Gizmos.DrawWireSphere(transform.position + Vector3.down * castDistance, circleRadius);
    }
}