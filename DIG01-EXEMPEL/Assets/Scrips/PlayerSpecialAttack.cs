using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    public float spinDuration = 1f; // Time taken for a full 360° spin
    public float fallSpeed = 20f;   // Speed of the fast fall
    public float damageRadius = 5f; // Radius of the area damage
    public int damageAmount = 50;   // Damage dealt to each enemy

    private Rigidbody2D rb;
    private bool isPerformingMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Trigger the "groundsmash attack" with a specific key
        if (Input.GetKeyDown(KeyCode.S) && !isPerformingMove)
        {
            StartCoroutine(PerformSpecialMove());
        }
    }

    private IEnumerator PerformSpecialMove()
    {
        isPerformingMove = true;

        //Stop the player in midair
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        //Spin character 360 degrees
        float elapsed = 0f;
        while (elapsed < spinDuration)
        {
            float rotationAmount = 360f * (Time.deltaTime / spinDuration);
            transform.Rotate(0, 0, rotationAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Fall fast to the ground
        rb.gravityScale = 1; // Reset gravity to default
        rb.velocity = new Vector2(0, -fallSpeed);

        // Wait until the player hits the ground
        while (!PlayerMovement.isGrounded)
        {
            yield return null;
        }

        // Step 4: Damage all enemies in the area
        //DamageEnemiesInRadius();

        // Step 5: Reset state
        isPerformingMove = false;
    }
    /*
    private void DamageEnemiesInRadius()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
    */
    private void OnDrawGizmosSelected()
    {
        // Visualize the damage radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
