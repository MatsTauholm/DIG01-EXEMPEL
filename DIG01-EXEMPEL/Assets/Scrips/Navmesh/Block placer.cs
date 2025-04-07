using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab; // Assign a block prefab in the inspector
    [SerializeField] LayerMask blockLayer;   // Assign a layer for blocks (e.g., "Blocks")

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 gridPos = new Vector2(Mathf.Round(mouseWorldPos.x), Mathf.Round(mouseWorldPos.y));

            Collider2D hit = Physics2D.OverlapPoint(gridPos, blockLayer);

            if (hit != null)
            {
                // Block exists, remove it
                Destroy(hit.gameObject);
            }
            else
            {
                // No block, place one
                Instantiate(blockPrefab, gridPos, Quaternion.identity);
            }
        }
    }
}
