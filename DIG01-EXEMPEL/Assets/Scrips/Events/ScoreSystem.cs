using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int Score { get; private set; }

    private void OnEnable()
    {
        GameEvents.onEnemyDestroyed += AddScore;
    }

    private void OnDisable()
    {
        GameEvents.onEnemyDestroyed -= AddScore;
    }

    private void Start()
    {
        GameEvents.onScoreChanged?.Invoke(Score);
    }

    private void AddScore(int points)
    {
        Score += Mathf.Max(0, points);
        GameEvents.onScoreChanged?.Invoke(Score);
    }
}

