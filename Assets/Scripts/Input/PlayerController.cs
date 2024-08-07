using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	private Vector2 _input;
	private CharacterController _characterController;
	private Vector3 _direction;
	[SerializeField] private float _moveSpeed = 6;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		// Move the character
		_characterController.Move(_direction * _moveSpeed * Time.deltaTime);
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		_input = context.ReadValue<Vector2>();
		_input = _input.normalized;
		
		_direction = Direcao(_input);
	}

	private Vector3 Direcao(Vector2 Input)
	{
		Vector3 retornodirecao = new Vector3(Input.x, 0, Input.y);
		return retornodirecao;
	}
}
