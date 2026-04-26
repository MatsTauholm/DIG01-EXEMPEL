using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy" ))
        {
            GameEvents.onEnemyHit?.Invoke(other.gameObject);
        }
        Destroy(gameObject);
    }
}