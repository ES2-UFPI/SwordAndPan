using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			Destroy(other.gameObject);
		}
	}
}
