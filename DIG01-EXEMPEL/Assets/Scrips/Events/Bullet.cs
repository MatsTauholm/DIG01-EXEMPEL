using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Min(0.05f)] public float lifeTimeSeconds = 2.5f;

    private void Awake()
    {
        Destroy(gameObject, lifeTimeSeconds);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null) return;

        GameEvents.onEnemyHit?.Invoke(enemy.gameObject, enemy.points);
        Destroy(gameObject);
    }
}

