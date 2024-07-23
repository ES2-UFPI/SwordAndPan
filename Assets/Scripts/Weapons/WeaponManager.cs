using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponManager : MonoBehaviour
{
	[SerializeField] private List<GameObject> weapons;
	[SerializeField] private Transform playerHand;
	private int activeWeaponIndex;
	private GameObject currentWeapon;

	private void Start()
	{
		transform.SetParent(playerHand);

		// Ativa a primeira arma
		if (weapons.Count > 0)
		{
			SwitchWeapon(0); 
			activeWeaponIndex = 0;
		}
	}



	private void Update()
	{
		// Alterna para a próxima arma quando a tecla '1' é pressionada
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwitchWeapon(0); // Ativa a primeira arma
		}

		// Alterna para a arma anterior quando a tecla '2' é pressionada
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchWeapon(1); // Ativa a segunda arma
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchWeapon(2); // Ativa a segunda arma
		}
	}

	private void SwitchWeapon(int index)
	{
		// Verifica se o índice é válido
		if (index < 0 || index >= weapons.Count)
		{
			Debug.LogError("Invalid weapon index!");
			return;
		}
		
		if (currentWeapon != null)
		{
			Destroy(currentWeapon);
		}
		currentWeapon = Instantiate(weapons[index], transform.position, transform.rotation, transform);

		// Atualiza o índice da arma ativa
		activeWeaponIndex = index;
	}
}
