using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private void OnEnable()
    {
        GameEvents.onScoreChanged += UpdateText;
    }

    private void OnDisable()
    {
        GameEvents.onScoreChanged -= UpdateText;
    }

    private void UpdateText(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}

