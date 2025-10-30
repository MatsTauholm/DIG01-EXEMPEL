using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private float explosionRadius = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyEnemies();
        } 
    }

    void DestroyEnemies()
    {
        //Kollar efter alla colliders inom cirkeln och lägger till dessa i en array
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer); 

        foreach (Collider2D enemy in hitEnemies) //Kolla igenom hela array:n       
        {
             Destroy(enemy.gameObject); //Förstör de objektet som hittas
        }

    }

    // Rita ut en cirkel i scenefönstret för enklare debug och balansering
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
