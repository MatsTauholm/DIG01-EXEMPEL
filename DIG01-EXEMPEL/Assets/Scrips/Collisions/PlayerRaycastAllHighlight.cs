using UnityEngine;
using System.Collections.Generic;

public class PlayerRaycastAllHighlight : MonoBehaviour
{
    public float maxDistance = 10f;
    public LayerMask hitLayers;

    private List<GameObject> lastHighlighted = new List<GameObject>();

    void Update()
    {
        // Get mouse position in world
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Direction from player to mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Perform raycast all
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, maxDistance, hitLayers);

        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

        // Clear previous highlights
        ClearHighlights();

        // Highlight every object hit
        foreach (RaycastHit2D hit in hits)
        {
            GameObject obj = hit.collider.gameObject;
            Highlight(obj);
            lastHighlighted.Add(obj);
        }
    }

    void Highlight(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.yellow;
    }

    void ClearHighlights()
    {
        foreach (GameObject obj in lastHighlighted)
        {
            if (obj != null)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.color = Color.red;
            }
        }
        lastHighlighted.Clear();
    }
}
