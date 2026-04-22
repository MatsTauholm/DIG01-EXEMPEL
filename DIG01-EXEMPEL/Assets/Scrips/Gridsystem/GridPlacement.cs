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
        previewInstance = Instantiate(previewObjectPrefab);
    }

    void OnClick(InputValue button)
    {
        if (!occupiedPositions.Contains(snappedPosition))
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        snappedMousePosition = new Vector2(
            Mathf.Round(mousePosition.x / gridSize) * gridSize,
            Mathf.Round(mousePosition.y / gridSize) * gridSize
        );

        if (occupiedPositions.Contains(snappedMousePosition))
        {  
            return;
        }

        Instantiate(objectToPlace, snappedMousePosition, Quaternion.identity);
        occupiedPositions.Add(snappedMousePosition);
    }

    void Update()
    {
        previewInstance.transform.position = snappedPosition;
        bool isOccupied = occupiedPositions.Contains(snappedPosition);
        UpdatePreviewColor(isOccupied);
    }

    private void UpdatePreviewColor(bool isOccupied)
    {
        SpriteRenderer sr = previewInstance.GetComponent<SpriteRenderer>();

        if (isOccupied)
            sr.color = new Color(1f, 0f, 0f, 0.5f); // red
        else
            sr.color = new Color(0f, 1f, 0f, 0.5f); // green
    }
}
