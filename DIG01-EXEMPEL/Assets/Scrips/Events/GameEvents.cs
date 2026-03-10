using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int> onPlayerDamaged;
    public static Action<int> onHealthChanged;
    public static Action onPlayerDeath;
}
