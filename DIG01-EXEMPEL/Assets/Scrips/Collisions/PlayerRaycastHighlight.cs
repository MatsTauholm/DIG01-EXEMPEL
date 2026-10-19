using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycastHighlight : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private bool checkAllHits = false;
    private List<GameObject> lastHighlightedObjects = new List<GameObject>();

    void Update()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        //Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f; // ensure z=0 in 2D

        //Calculate the direction from player to mouse
        Vector2 direction = mousePos - transform.position;

        //Perform the raycast
        if (checkAllHits)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, maxDistance, hitLayers);
            HandleHits(hits);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, hitLayers);
            HandleHit(hit);
        }

        //Debug visualization
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);
    }

    private void HandleHit(RaycastHit2D hit)
    {
        // If we hit something
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log($"New hit detected: {hitObject.name}");
            lastHighlightedObjects.Add(hitObject);
            Highlight(hitObject);
        }
        else
        {
            //No hit, clear any previous highlight
            ClearHighlight();
        }
    }
    private void HandleHits(RaycastHit2D[] hits)
    {
        if (hits != null)
        {
            Debug.Log($"Number of hits: {hits.Length}");
            foreach (RaycastHit2D hit in hits)
            {
                GameObject hitObject = hit.collider.gameObject;
                Highlight(hitObject);
                lastHighlightedObjects.Add(hitObject);
            }

            if (hits.Length == 0)
            {
                //No hit, clear any previous highlight
                ClearHighlight();
            }
        }
    }

    void Highlight(GameObject hitObject) // Highlight the hit object by changing its color to yellow
    {
        SpriteRenderer sr = hitObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.yellow; // highlight color
        }   
    }
    void ClearHighlight() // Reset the color of previously highlighted objects back to red
    {
        foreach (GameObject obj in lastHighlightedObjects)
        {
            if (obj != null)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.red;
                }
                    
            }
        }
        lastHighlightedObjects.Clear(); // Clear the list after resetting colors
    }
}
