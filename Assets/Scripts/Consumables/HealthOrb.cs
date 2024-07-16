using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
	[SerializeField] private float restoreAmount = 20f; 

	private void OnTriggerEnter(Collider other)
	{
		PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
		if (playerStatus != null)
		{
			playerStatus.RestoreHealth(restoreAmount);
			Destroy(gameObject); 
		}
	}
}
