using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    private void Start()
    {
        var healthSystem = FindFirstObjectByType<HealthSystem>();
        if (healthSystem != null && healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = healthSystem.health;
            UpdateHealthBar(healthSystem.health);
        }
    }

    private void OnEnable()
    {
        GameEvents.onHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        GameEvents.onHealthChanged -= UpdateHealthBar;
    }


    public void UpdateHealthBar(int health)
    {
        if (healthBar == null || fill == null)
        {
            return;
        }

        healthBar.value = Mathf.Clamp(health, (int)healthBar.minValue, (int)healthBar.maxValue);
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}
