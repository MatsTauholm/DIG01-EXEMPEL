using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float normalZoom = 5f;
    [SerializeField] private float zoomedIn = 3f;
    [SerializeField] private float zoomSpeed = 5f;

    [Header("Input")]
    [SerializeField] private InputActionReference zoomAction;

    private void Update()
    {
        float targetZoom;

        if (zoomAction.action.IsPressed())
        {
            targetZoom = zoomedIn;
        }
        else
        {
            targetZoom = normalZoom;
        }

        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize,
            targetZoom,
            zoomSpeed * Time.deltaTime
        );
    }
}