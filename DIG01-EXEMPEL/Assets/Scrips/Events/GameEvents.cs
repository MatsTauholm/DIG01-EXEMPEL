using System;
using UnityEngine;

public static class GameEvents
{
    //Player / Health
    public static Action<int> onPlayerDamaged;
    public static Action<int> onHealthChanged;
    public static Action onPlayerDeath;

    // Shooting / scoring
    public static Action<GameObject> onEnemyHit; // (enemy gameObject)
    public static Action<int> onEnemyDestroyed; // (points)
    public static Action<int> onScoreChanged; // (newScore)

    //Effects
    public static Action<GameObject> onPlayParticle; // (effect gameObject)
    public static Action<GameObject> onPlayVFX; // (effect gameObject)
    public static Action<GameObject> onPlaySFX; // (effect gameObject)
    public static Action<GameObject> onPlayMusic; // (effect gameObject)

}
