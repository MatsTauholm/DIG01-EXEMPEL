using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlacement : MonoBehaviour
{
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private GameObject previewObjectPrefab;

    private Vector2 snappedPosition;
    private Vector2 snappedMousePosition;
    private GameObject previewInstance;
    private HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

    void Start()
    {
        // Create a preview instance that will follow the mouse cursor and show where the object will be placed
        previewInstance = Instantiate(previewObjectPrefab);
    }

    void OnClick(InputValue button)
    {
        if (!occupiedPositions.Contains(snappedPosition)) //Check if the current snapped position is not occupied before placing the object
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // Get the mouse position in world coordinates
        //Snap the mouse position to the nearest grid point by rounding it to the nearest multiple of gridSize
        snappedMousePosition = new Vector2(
            Mathf.Round(mousePosition.x / gridSize) * gridSize,
            Mathf.Round(mousePosition.y / gridSize) * gridSize
        ); 

        if (occupiedPositions.Contains(snappedMousePosition)) //Another check to ensure that the snapped mouse position is not already occupied before instantiating the object
        {  
            return;
        }

        Instantiate(objectToPlace, snappedMousePosition, Quaternion.identity); // Instantiate the object at the snapped mouse position
        occupiedPositions.Add(snappedMousePosition); // Add the snapped mouse position to the set of occupied positions to prevent future placements at the same location
    }

    void Update()
    {
        previewInstance.transform.position = snappedPosition; // Update the position of the preview instance to follow the mouse cursor
        bool isOccupied = occupiedPositions.Contains(snappedPosition); //Set bool if the current snapped position is occupied
        UpdatePreviewColor(isOccupied);
    }

    private void UpdatePreviewColor(bool isOccupied) //Change the color of the preview instance based on whether the current snapped position is occupied or not
    {
        SpriteRenderer sr = previewInstance.GetComponent<SpriteRenderer>();

        if (isOccupied)
            sr.color = new Color(1f, 0f, 0f, 0.5f); // red
        else
            sr.color = new Color(0f, 1f, 0f, 0.5f); // green
    }
}
