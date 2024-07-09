using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleAI : MonoBehaviour
{
    [Header("Enemy Settings")]
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float gravityMultiplier = 2f;
	[SerializeField] private float movementSpeed = 5f;
	private Vector3 enemyVelocity;
    private CharacterController characterController;

    public Transform player;
    public float stoppingDistance = 3f;
    private bool isWalking;

    private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		
		// Adjust the center of the CharacterController to be in the middle
		Vector3 newCenter = characterController.center;
		newCenter.y = characterController.height / 2;
		characterController.center = newCenter;
	}

    private void Start()
    {

    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > stoppingDistance)
            {
                HandleMovement();
            }

            ApplyGravity();
        }
    }

    private void HandleMovement()
	{
		Vector3 movementDirection = new Vector3(player.position.x - transform.position.x, 0f, player.position.y - transform.position.y).normalized;

		Vector3 horizontalVelocity = movementDirection * movementSpeed;
		enemyVelocity.x = horizontalVelocity.x;
		enemyVelocity.z = horizontalVelocity.z;

		characterController.Move(enemyVelocity * Time.deltaTime);

		isWalking = movementDirection != Vector3.zero;

		float rotateSpeed = 10f;

		if (movementDirection != Vector3.zero)
		{
			transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
		}
	}

    private void ApplyGravity()
	{
		if (characterController.isGrounded)
		{
			enemyVelocity.y = -1.0f;
		}
		else
		{
			enemyVelocity.y += gravityMultiplier * gravity * Time.deltaTime;
		}
	}

    public bool IsWalking()
	{
		return isWalking;
	}
}
