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

    private void OnEnemyHit(GameObject hitEnemy, int awardedPoints)
    {
        if (hitEnemy != gameObject) return;

        GameEvents.onEnemyDestroyed?.Invoke(awardedPoints);
        Destroy(gameObject);
    }
}

