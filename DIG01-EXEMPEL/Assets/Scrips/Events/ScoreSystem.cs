using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    public int Score { get; private set; }
    [SerializeField] TMP_Text scoreText;

    private void OnEnable()
    {
        GameEvents.onEnemyDestroyed += AddScore;
        GameEvents.onScoreChanged += UpdateText;
    }

    private void OnDisable()
    {
        GameEvents.onEnemyDestroyed -= AddScore;
        GameEvents.onScoreChanged -= UpdateText;
    }

    private void Start()
    {
        GameEvents.onScoreChanged?.Invoke(Score);
    }

    private void AddScore(int points)
    {
        Score += points;
        GameEvents.onScoreChanged?.Invoke(Score);
    }
        private void UpdateText(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}

