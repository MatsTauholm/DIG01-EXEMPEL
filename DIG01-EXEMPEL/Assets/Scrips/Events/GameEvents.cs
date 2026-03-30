using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int> onPlayerDamaged;
    public static Action<int> onHealthChanged;
    public static Action onPlayerDeath;

    // Shooting / scoring
    public static Action<GameObject, int> onEnemyHit; // (enemy gameObject, points)
    public static Action<int> onEnemyDestroyed; // (points)
    public static Action<int> onScoreChanged; // (newScore)
}
