using System;
using UnityEngine;
using NavMeshPlus.Components;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab; // Assign a block prefab in the inspector
    [SerializeField] LayerMask blockLayer;   // Assign a layer for blocks (e.g., "Blocks")
    [SerializeField] NavMeshSurface playerSurface;
    [SerializeField] NavMeshSurface enemySurface;

    void Start()
    {
        UpdateNavmesh();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            PlaceBlock();   
        }
    }

    private void PlaceBlock()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector2 gridPos = new Vector2(Mathf.Round(mouseWorldPos.x), Mathf.Round(mouseWorldPos.y)); // Snap to grid (since blocks are 1 unit in size)

        Collider2D hit = Physics2D.OverlapPoint(gridPos, blockLayer); // Check if there's already a block at this position

        if (hit != null)
        {
            // Block exists, remove it and update navmesh
            Destroy(hit.gameObject);
            UpdateNavmesh();
        }
        else
        {
            // No block, place one and update navmesh
            Instantiate(blockPrefab, gridPos, Quaternion.identity);
            UpdateNavmesh();
        }
    }

    // This method updates the navmesh after placing or removing a block
    private void UpdateNavmesh() 
    {
        playerSurface.UpdateNavMesh(playerSurface.navMeshData);
        enemySurface.UpdateNavMesh(enemySurface.navMeshData);
    }
}
