using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroleWithCoroutine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 2f;

    Coroutine patrol;
    private Vector3 target;

    void Start()
    {
        target = pointB.position;
        patrol = StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        //Keep looping the couroutine 
        while (true) 
        {
            // Move towards target until close enough
            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //Start moving
                yield return null; // Wait for next frame
            }

            // Wait at the point
            yield return new WaitForSeconds(waitTime);

            // Switch target
            {
                if (target == pointA.position)
                {
                    target = pointB.position;
                }
                else
                {
                    target = pointA.position;
                }
            }
        }
    }
}
