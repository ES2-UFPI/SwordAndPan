using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
	[SerializeField] private ParticleSystem knifeParticle;
	[SerializeField] private GameInput gameInput; // Referência à entrada do jogo

	private void Start()
	{
		// Procura pelo objeto gameInput na cena
		GameObject gameInputObject = GameObject.Find("GameInput");
		// Se o objeto for encontrado, tenta pegar o script GameInput
		if (gameInputObject != null)
		{
			gameInput = gameInputObject.GetComponent<GameInput>();
			if (gameInput != null)
			{
				gameInput.OnFireAction += OnFire; // Subscreve ao evento de ataque
			}
			else
			{
				Debug.LogError("GameInput script not found on gameInput object!");
			}
		}
		else
		{
			Debug.LogError("gameInput object not found in the scene!");
		}
	}


	private void OnFire(object sender, EventArgs e)
	{
		knifeParticle.Play();
		Debug.Log("Ativou o efeito");
		ActivateAttackParticle();
	}

	private void ActivateAttackParticle()
	{
		knifeParticle.Play();
	}
}
