using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEvents.onPlayerDamaged?.Invoke(damage);
        }
    }
}
