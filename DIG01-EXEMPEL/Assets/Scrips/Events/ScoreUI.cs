using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private void Awake()
    {
        if (scoreText == null)
            scoreText = CreateDefaultText();
    }

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

    private TMP_Text CreateDefaultText()
    {
        var canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null) return null;

        var go = new GameObject("Score Text");
        go.transform.SetParent(canvas.transform, false);

        var rt = go.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(1f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(1f, 1f);
        rt.anchoredPosition = new Vector2(-24f, -24f);
        rt.sizeDelta = new Vector2(300f, 60f);

        var tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.fontSize = 32;
        tmp.alignment = TextAlignmentOptions.TopRight;
        tmp.text = "Score: 0";
        tmp.raycastTarget = false;
        return tmp;
    }
}

