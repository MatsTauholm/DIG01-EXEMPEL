using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshFollow : MonoBehaviour
{
    [SerializeField] Transform target; //Referens to player
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //Removes unwanted rotation
        agent.updateUpAxis = false; //Removes unwanted rotation
    }

    void Update()
    {
        agent.SetDestination(target.position); //Move to target (Player) position)
    }
}
