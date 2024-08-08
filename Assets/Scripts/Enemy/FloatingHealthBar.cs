using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private new Camera camera;
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offSet;

	private void Update()
	{
		UpdateHealthBarUI();
	}

	public void UpdateHealthBar(float currentValue, float maxValue)
	{
		slider.value = currentValue / maxValue;
	}

	private void UpdateHealthBarUI()
	{
		transform.rotation = camera.transform.rotation;
		transform.position = target.position + offSet;
	}
}
