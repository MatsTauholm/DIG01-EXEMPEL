using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputInteractions : MonoBehaviour
{

    [Header("Camera Zoom Settings")]
    [SerializeField] private float normalZoom = 5f;
    [SerializeField] private float zoomedIn = 3f;
    [SerializeField] private float zoomSpeed = 5f;
    private float targetZoom;

    [Header("Gun Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject superBulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletSpread = 15f;
    private bool isAutomaticFireEnabled = false;

    private void Start()
    {
        targetZoom = normalZoom;
    }

    public void OnAutomaticFire(InputAction.CallbackContext context)
    {
        isAutomaticFireEnabled = context.ReadValueAsButton();
    }

    public void OnFire(InputAction.CallbackContext context)
    {

        // A Hold interaction completed
        if (context.interaction is HoldInteraction)
        {
            if (context.performed)
            {
                targetZoom = zoomedIn;
                Debug.Log("Started holding!");
            }
            else if (context.canceled)
            {
                targetZoom = normalZoom;
                Debug.Log("Stopped holding!");
            }
        }

        // A Tap interaction completed
        if (context.performed &&
            context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            Shoot(bulletPrefab);
            Debug.Log("Performed a Tap!");
        }

        // A MultiTap interaction completed
        if (context.performed &&
            context.interaction is UnityEngine.InputSystem.Interactions.MultiTapInteraction)
        {
            MultiShoot();
            Debug.Log("Multi Tapping!");
        }

        // A SlowTap interaction completed
        if (context.canceled &&
            context.interaction is UnityEngine.InputSystem.Interactions.SlowTapInteraction)
        {
            Shoot(superBulletPrefab);
            Debug.Log("Performed a Slow Tap!");
        }
    }        

    private void Update()
    {
        AutomaticFire();
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        Camera.main.orthographicSize = Mathf.Lerp(
        Camera.main.orthographicSize,
        targetZoom,
        zoomSpeed * Time.deltaTime);
    }

    private void AutomaticFire()
    {
        if (isAutomaticFireEnabled)
        {
            Shoot(bulletPrefab);
        }
    }

    private void Shoot(GameObject bulletPrefab)
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * bulletSpeed;
    }

    private void MultiShoot()
    {
        for (int i = -1; i <= 1; i++)
        {
            float angle = i * bulletSpread;

            Vector2 shotDirection = Quaternion.Euler(0, 0, angle) * firePoint.right;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = shotDirection * bulletSpeed;
        }
    }
}
