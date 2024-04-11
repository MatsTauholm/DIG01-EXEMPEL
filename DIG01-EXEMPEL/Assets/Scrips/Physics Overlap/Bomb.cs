using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;

    void OnFire()
    {
        DestroyEnemies();
    }

    void DestroyEnemies()
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemies"); //Referens till Enemies layer
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer); //Kollar efter alla colliders inom cirkeln och lägger till dessa i en array

        foreach (Collider2D enemy in hitEnemies) //Kolla igenom hela array:n       
        {
             Destroy(enemy.gameObject); //Förstör objektet
        }

    }

    // Rita ut en cirkel i scenefönstret för enklare debug och balansering
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
