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
    [SerializeField] float bulletSpeed = 25f;
    [SerializeField] float fireCooldownSeconds = 0.15f;
    [SerializeField] float bulletLifetimeSeconds = 2.5f;

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
        if (muzzle == null) muzzle = transform;
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
        if (!value.isPressed) return;
        if (Time.timeAsDouble < nextFireTime) return;
        nextFireTime = Time.timeAsDouble + fireCooldownSeconds;
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

    private void FireBullet()
    {
        var bulletGo = new GameObject("Bullet");
        bulletGo.transform.position = muzzle.position;
        bulletGo.transform.rotation = transform.rotation;

        var bulletRb = bulletGo.AddComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0f;
        bulletRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        bulletRb.linearVelocity = (Vector2)transform.up * bulletSpeed;

        var col = bulletGo.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = 0.1f;

        var bullet = bulletGo.AddComponent<Bullet>();
        bullet.lifeTimeSeconds = bulletLifetimeSeconds;
    }
}