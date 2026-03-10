using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

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
        healthBar.value = health;

        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }



}
