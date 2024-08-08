using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100f; // Initial health value of the player

    public bool IsDamaged = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
        else
        {
            IsDamaged = false;
        }
    }

    public void TakeDamage(float damage)
    {
        IsDamaged = true;
        playerHealth -= damage;

        Debug.Log($"Player took {damage} damage. Current health: {playerHealth}");

        if (playerHealth <= 0)
        {
            Debug.Log("Player has died!");
        }
    }

    public void RestoreHealth(float health)
    {
        playerHealth += health;

        if (playerHealth > 100f)
        {
            playerHealth = 100f;
        }

        Debug.Log($"Player restored {health} health. Current health: {playerHealth}");
    }

    public bool GetIsDamaged() => IsDamaged;

    public float GetCurrentHealth() => playerHealth; // Add this method to access the player's current health
}