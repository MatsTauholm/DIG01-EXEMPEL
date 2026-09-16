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

    private void Start()
    {
        targetZoom = normalZoom;
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

             

//// Button released before completing a SlowTap
//if (context.canceled)
//{
//    Shoot(bulletPrefab);
//    targetZoom = normalZoom;
//    Debug.Log("Stopped charging");
//}

//public void OnHold(InputAction.CallbackContext ctx)
//{
//    if (ctx.performed)
//    {
//        targetZoom = zoomedIn;
//        Debug.Log("Holding!");
//    }

//    if (ctx.canceled)
//    {
//        targetZoom = normalZoom;
//        Debug.Log("Held for " + ctx.duration + " seconds.");
//    }
//}

//public void OnMultiTap(InputAction.CallbackContext ctx)
//{
//    if (ctx.performed)
//    {
//        Debug.Log("Multi Tapped!");
//    }
//}

//public void OnTap(InputAction.CallbackContext ctx)
//{
//    if (ctx.performed)
//    {
//        Debug.Log("Tapped!");
//    }
//}

//public void OnSlowTap(InputAction.CallbackContext ctx)
//{
//    if (ctx.performed)
//    {
//        Shoot(superBulletPrefab);
//        Debug.Log("Slow Tapped!");
//    }
//}

private void Update()
    {
        Camera.main.orthographicSize = Mathf.Lerp(
        Camera.main.orthographicSize,
        targetZoom,
        zoomSpeed * Time.deltaTime) ;
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
