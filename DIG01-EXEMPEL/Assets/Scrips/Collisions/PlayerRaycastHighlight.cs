using UnityEngine;

public class PlayerRaycastHighlight : MonoBehaviour
{
    [SerializeField] float maxDistance = 10f;
    [SerializeField] LayerMask hitLayers;
    private bool isShooting;
    private GameObject lastHighlighted;

    void OnAttack()
    {
        isShooting = true;
    }

    void Update()
    {
        if (isShooting)
        {
            Shoot();
            isShooting = false;
        }

    }

    void Shoot()
    {
        //Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // ensure z=0 in 2D

        //Calculate the direction from player to mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        //Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, hitLayers);

        //Debug visualization
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

        // If we hit something
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            // If it's a new object, highlight it
            if (hitObject != lastHighlighted)
            {
                ClearHighlight();
                Highlight(hitObject);
                lastHighlighted = hitObject;
            }
        }
        else
        {
            // No hit, clear any previous highlight
            ClearHighlight();
        }
    }

    void Highlight(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.yellow; // highlight color
    }

    void ClearHighlight()
    {
        if (lastHighlighted != null)
        {
            SpriteRenderer sr = lastHighlighted.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.red; // reset to normal color
            lastHighlighted = null;
        }
    }
}
