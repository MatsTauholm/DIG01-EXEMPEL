using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePatrole : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float waitTime = 2f;
    public float lookAroundAngle = 45f;
    public float lookSpeed = 90f;

    private Transform targetPoint;

    private void Start()
    {
        targetPoint = pointB;
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // Move toward target point
            yield return StartCoroutine(MoveToPoint(targetPoint.position));

            // Do a little look around
            yield return StartCoroutine(LookAround());

            // Switch target
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }

    IEnumerator MoveToPoint(Vector3 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.1f)
        {
            Vector2 direction = (target - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            // Face direction by flipping scale
            if (direction.x != 0)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
                transform.localScale = localScale;
            }

            yield return null;
        }
    }

    IEnumerator LookAround()
    {
        float startZ = transform.eulerAngles.z;

        // Look left
        yield return StartCoroutine(RotateToZ(startZ + lookAroundAngle));
        // Look right
        yield return StartCoroutine(RotateToZ(startZ - lookAroundAngle));
        // Return to original
        yield return StartCoroutine(RotateToZ(startZ));

        yield return new WaitForSeconds(waitTime);
    }

    IEnumerator RotateToZ(float targetZ)
    {
        // Normalize target rotation
        float currentZ = transform.eulerAngles.z;
        float angle = Mathf.DeltaAngle(currentZ, targetZ);

        while (Mathf.Abs(angle) > 1f)
        {
            float step = lookSpeed * Time.deltaTime * Mathf.Sign(angle);
            transform.Rotate(0, 0, step);
            currentZ = transform.eulerAngles.z;
            angle = Mathf.DeltaAngle(currentZ, targetZ);
            yield return null;
        }

        // Snap to final rotation
        Vector3 angles = transform.eulerAngles;
        angles.z = targetZ;
        transform.eulerAngles = angles;
    }
}
