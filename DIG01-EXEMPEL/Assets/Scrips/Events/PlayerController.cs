using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]

    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float maxSpeed = 15f;

    [Header("Shooting Settings")]
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 25f;

    [Header("Screen Wrapping")]
    [SerializeField] bool screenWrap = true;
    [SerializeField] float screenPadding = 0.5f;

    private Rigidbody2D rb;
    private Camera mainCam;

    private float rotateInput;
    private bool thrusting;
    private double nextFireTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }
    

    public void OnRotate(InputValue value)
    {
        rotateInput = value.Get<float>();
    }

    public void OnThrust(InputValue value)
    {
        thrusting = value.isPressed;
    }

    public void OnFire(InputValue value)
    {
        FireBullet();
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleThrust();
        LimitSpeed();
    }

    private void LateUpdate()
    {
        if (screenWrap)
            WrapScreen();
    }

    void HandleRotation()
    {
        rb.rotation -= rotateInput * rotationSpeed * Time.fixedDeltaTime;
    }

    void HandleThrust()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustForce);
        }
    }

    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void FireBullet()
    {
        var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = muzzle.up * bulletSpeed;
    }

    void WrapScreen()
    {
        Vector3 screenPos = mainCam.WorldToViewportPoint(transform.position);

        if (screenPos.x > 1 + screenPadding)
            screenPos.x = -screenPadding;
        else if (screenPos.x < -screenPadding)
            screenPos.x = 1 + screenPadding;

        if (screenPos.y > 1 + screenPadding)
            screenPos.y = -screenPadding;
        else if (screenPos.y < -screenPadding)
            screenPos.y = 1 + screenPadding;

        transform.position = mainCam.ViewportToWorldPoint(screenPos);
    }
}