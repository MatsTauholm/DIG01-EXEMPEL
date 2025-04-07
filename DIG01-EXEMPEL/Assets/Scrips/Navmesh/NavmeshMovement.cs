using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshMovement : MonoBehaviour
{
    public Camera mainCamera;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; //Removes rotation
        agent.updateUpAxis = false; //Removes rotation
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            MoveToMouseClick();
        }
    }

    void MoveToMouseClick()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Checks position of mouse click

        NavMeshHit hit; //Result information for NavMesh queries
        if (NavMesh.SamplePosition(mouseWorldPosition, out hit, 0.5f, NavMesh.AllAreas)) //Find the nearest hit on Navmesh from the mouse click
        {
            agent.SetDestination(hit.position); //Moves the agent to the position using the settings in the Nav mesh Agent component
        }
    }
}
