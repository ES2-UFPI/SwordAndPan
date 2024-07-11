using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField] private float playerHealth = 100f; // Valor inicial da saúde do jogador
	[SerializeField] private float damagePlayer = 10f; // Dano causado pelo jogador

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(damagePlayer);
		}
	}

	public void TakeDamage(float damage)
	{
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

}
