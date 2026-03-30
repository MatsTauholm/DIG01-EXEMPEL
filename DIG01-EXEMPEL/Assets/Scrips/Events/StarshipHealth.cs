using System;
using UnityEngine;

public class StarshipHealth : MonoBehaviour
{
    public int health = 100;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = health;
        GameEvents.onHealthChanged?.Invoke(currentHealth);
    }

    private void OnEnable()
    {
        GameEvents.onPlayerDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        GameEvents.onPlayerDamaged -= TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GameEvents.onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            GameEvents.onPlayerDeath?.Invoke();
        }
    }

}