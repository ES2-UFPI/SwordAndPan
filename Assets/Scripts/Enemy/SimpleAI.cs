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
		// Vector3 newCenter = characterController.center;
		// newCenter.y = characterController.height / 2;
		// characterController.center = newCenter;
	}

    private void Start()
    {

    }

    private void Update()
    {
        if (player != null)
        {
            ApplyGravity();

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > stoppingDistance)
            {
                Pathfinding();
            }
        }
    }

    private void HandleMovement()
	{
		Vector3 movementDirection = new Vector3(player.position.x - transform.position.x, 0f, player.position.z - transform.position.z).normalized;

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
            characterController.Move(enemyVelocity * Time.deltaTime);
		}
		else
		{
			enemyVelocity.y += gravityMultiplier * gravity * Time.deltaTime;
            characterController.Move(enemyVelocity * Time.deltaTime);
		}
	}

	// A ideia é um sistema de pathfinding simples que funciona assim:
	// 1 - Tenta andar diretamente até o player
	// 2 - Se não puder, anda diretamente até a posição X do player
	// 3 - Se não puder, anda diretamente até a posição Z do player
	// 4 - Se não puder, anda diretamente
	// Não está finalizado
	private void Pathfinding()
	{
		Vector3 direction = (player.position - transform.position).normalized;

        // Try to move directly towards the player
        if (CanMoveInDirection(direction))
        {
            MoveInDirection(direction);
        }
        else
        {
            // Try to move towards the player's X position
            Vector3 directionToPlayerX = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
            if (CanMoveInDirection(directionToPlayerX))
            {
                MoveInDirection(directionToPlayerX);
            }
            else
            {
                // Try to move towards the player's Z position
                Vector3 directionToPlayerZ = new Vector3(0, 0, player.position.z - transform.position.z).normalized;
                if (CanMoveInDirection(directionToPlayerZ))
                {
                    MoveInDirection(directionToPlayerZ);
                }
            }
        }
	}

	private bool CanMoveInDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 3.5f))
        {
            // If the ray hits something, check if it's not the player
            if (hit.collider.name != "Player")
            {
                return false;
            }
        }
        return true;
    }

    private void MoveInDirection(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    public bool IsWalking()
	{
		return isWalking;
	}
}
