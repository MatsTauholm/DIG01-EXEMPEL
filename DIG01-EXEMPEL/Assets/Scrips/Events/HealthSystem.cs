using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 100;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }

    private void Start()
    {
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
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");
        GameEvents.onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            GameEvents.onPlayerDeath?.Invoke();
        }
    }

}