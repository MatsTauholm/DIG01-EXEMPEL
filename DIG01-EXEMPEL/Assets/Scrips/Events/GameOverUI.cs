using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.onPlayerDeath += ShowText;
    }

    private void OnDisable()
    {
        GameEvents.onPlayerDeath -= ShowText;
    }

    void ShowText()
    {
        gameObject.SetActive(true);
    }
}
