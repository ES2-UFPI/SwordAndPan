using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float gravityMultiplier = 2f;
	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private float rollSpeed = 2f; // Velocidade do rolamento
	private Vector3 playerVelocity;
	private CharacterController characterController;
	[SerializeField] private GameInput gameInput;

	[Header("Interaction Settings")]
	[SerializeField] private Transform interactorSource;
	[SerializeField] private float interactRange = 3f;
	[SerializeField] private LayerMask interactableLayer;

	private bool isWalking;
	private bool isRolling;
	private bool isInvincible;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();

		// Ajusta o centro do CharacterController para estar no meio
		Vector3 newCenter = characterController.center;
		newCenter.y = characterController.height / 2;
		characterController.center = newCenter;
	}

	private void Start()
	{
		gameInput.OnInteractAction += OnInteract;
		gameInput.OnFireAction += OnFire;
		gameInput.OnRollAction += OnRoll; // Subscreve ao evento de rolamento
	}

	private void Update()
	{
		if (!isRolling)
		{
			HandleMovement();
			ApplyGravity();
		}
	}

	private void HandleMovement()
	{
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
		Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
		movementDirection = Quaternion.Euler(0, 45f, 0) * movementDirection;

		Vector3 horizontalVelocity = movementDirection * movementSpeed;
		playerVelocity.x = horizontalVelocity.x;
		playerVelocity.z = horizontalVelocity.z;

		characterController.Move(playerVelocity * Time.deltaTime);

		isWalking = movementDirection != Vector3.zero;

		float rotateSpeed = 10f;

		if (movementDirection != Vector3.zero)
		{
			transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
		}
	}

	private void OnInteract(object sender, EventArgs e)
	{
		Debug.Log("Player pressed Interact key.");

		Ray interactionRay = new Ray(interactorSource.position, interactorSource.forward);
		if (Physics.Raycast(interactionRay, out RaycastHit hitInfo, interactRange, interactableLayer))
		{
			IInteractable interactableObject = hitInfo.collider.GetComponent<IInteractable>();
			if (interactableObject != null)
			{
				interactableObject.OnInteract();
			}
		}
	}

	private void OnFire(object sender, EventArgs e)
	{
	}

	private void OnRoll(object sender, EventArgs e)
	{
		if (!isRolling)
		{
			StartCoroutine(Roll());
		}
	}

	private IEnumerator Roll()
	{
		isRolling = true; // Marca como rolando
		isInvincible = true; // Marca como invencível
		
		Vector3 rollDirection = transform.forward; // Direção do rolamento
		float rollDuration = 0f; // Duração do rolamento em segundos
		float elapsedTime = 0f;

		while (elapsedTime < rollDuration)
		{
			characterController.Move(rollSpeed * Time.deltaTime * rollDirection); // Movimenta o personagem na direção do rolamento
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		isRolling = false; // Marca como não rolando
		yield return new WaitForSeconds(0.2f); // Tempo de invencibilidade após o rolamento
		isInvincible = false; // Marca como não invencível
	}

	private void ApplyGravity()
	{
		if (characterController.isGrounded)
		{
			playerVelocity.y = -1.0f;
		}
		else
		{
			playerVelocity.y += gravityMultiplier * gravity * Time.deltaTime;
		}
	}

	public bool IsWalking()
	{
		return isWalking;
	}

	public bool IsInvincible()
	{
		return isInvincible;
	}
}
