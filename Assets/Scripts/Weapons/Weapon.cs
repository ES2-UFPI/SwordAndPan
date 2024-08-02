using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] private string weaponName;
	[SerializeField] private GameObject weaponModel;
	[SerializeField] private int damageWeapon;

	public float GetDamageWeapon() => damageWeapon;
}
