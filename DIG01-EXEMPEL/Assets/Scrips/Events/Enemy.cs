using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int points = 1;
    private void OnEnable()
    {
        GameEvents.onEnemyHit += OnEnemyHit;
    }
    private void OnDisable()
    {
        GameEvents.onEnemyHit -= OnEnemyHit;
    }

    private void OnEnemyHit(GameObject hitEnemy)
    {
        if (hitEnemy != gameObject) return;

        GameEvents.onEnemyDestroyed?.Invoke(points);
        Destroy(gameObject);
    }
}

