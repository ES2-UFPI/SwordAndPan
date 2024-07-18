using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float gravityMultiplier = 2f;
	[SerializeField] private float movementSpeed = 5f;
	private Vector3 playerVelocity;
	private CharacterController characterController;
	[SerializeField] private GameInput gameInput;

	[Header("Interaction Settings")]
	[SerializeField] private Transform interactorSource;
	[SerializeField] private float interactRange = 3f;
	[SerializeField] private LayerMask interactableLayer;
<<<<<<< Updated upstream

	[Header("Inventário")]
	public Inventario inventario;
	private bool isWalking;
=======
	
	private bool isWalking;	
	
	[Header("Inventário")]
	public Inventario inventario;
>>>>>>> Stashed changes

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		inventario = this.gameObject.AddComponent<Inventario>();
		// Adjust the center of the CharacterController to be in the middle
		Vector3 newCenter = characterController.center;
		newCenter.y = characterController.height / 2;
		characterController.center = newCenter;
	}

	private void Start()
	{
		gameInput.OnInteractAction += OnInteract;
	}

	private void Update()
	{
		HandleMovement();
		ApplyGravity();
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
			Debug.Log("chegou até essa parte " + hitInfo.collider.name);
			IInteractable interactableObject = hitInfo.collider.GetComponent<IInteractable>();
			if (interactableObject != null)
			{
<<<<<<< Updated upstream
				interactableObject.OnInteract(this, e);
=======
				Debug.Log("chegou até o final");
				interactableObject.OnInteract(this, e);
			}
			if (interactableObject == null)
			{
				Debug.Log("o objeto é nulo");
>>>>>>> Stashed changes
			}
		}
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
}
