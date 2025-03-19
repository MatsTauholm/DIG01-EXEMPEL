using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyEnemies();
        } 
    }

    void DestroyEnemies()
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemies"); //Referens till det layer som alla enemies ligger i
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer); //Kollar efter alla colliders inom cirkeln och lägger till dessa i en array

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
