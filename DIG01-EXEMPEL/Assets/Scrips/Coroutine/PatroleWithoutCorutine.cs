using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroleWithoutCorutine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 2f;

    private Vector3 target;
    private bool isWaiting = false;
    private float waitStartTime;

    void Start()
    {
        //Set start target
        target = pointB.position;
    }

    void Update()
    {
        if (isWaiting)
        {
            //Check if time since enemy stopped is more then waitTime
            if (Time.time - waitStartTime >= waitTime)
            {
                isWaiting = false;
                SwitchTarget();
            }
            return;
        }
        MoveToTarget();
    }

    void MoveToTarget()
    {
        //Move to target position
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Move towards target until close enough
        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            //Start waiting
            isWaiting = true;
            waitStartTime = Time.time;
        }
    }

    void SwitchTarget()
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
