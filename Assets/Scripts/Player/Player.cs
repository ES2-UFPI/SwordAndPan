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
    [SerializeField] private float rollSpeed = 2f;
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
        InitializeCharacterController();
    }

    private void Start()
    {
        SubscribeToGameInputEvents();
    }

    private void Update()
    {
        if (!isRolling)
        {
            HandleMovement();
            ApplyGravity();
        }
    }

    private void InitializeCharacterController()
    {
        characterController = GetComponent<CharacterController>();
        Vector3 newCenter = characterController.center;
        newCenter.y = characterController.height / 2;
        characterController.center = newCenter;
    }

    private void SubscribeToGameInputEvents()
    {
        gameInput.OnInteractAction += OnInteract;
        gameInput.OnFireAction += OnFire;
        gameInput.OnRollAction += OnRoll;
    }

    private void HandleMovement()
    {
        Vector3 movementDirection = GetMovementDirection();
        MoveCharacter(movementDirection);
        RotateCharacter(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        return Quaternion.Euler(0, 45f, 0) * movementDirection;
    }

    private void MoveCharacter(Vector3 movementDirection)
    {
        Vector3 horizontalVelocity = movementDirection * movementSpeed;
        playerVelocity.x = horizontalVelocity.x;
        playerVelocity.z = horizontalVelocity.z;
        characterController.Move(playerVelocity * Time.deltaTime);
        isWalking = movementDirection != Vector3.zero;
    }

    private void RotateCharacter(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
        }
    }

    private void OnInteract(object sender, EventArgs e)
    {
        Debug.Log("Player pressed Interact key.");
        TryInteract();
    }

    private void TryInteract()
    {
        Ray interactionRay = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(interactionRay, out RaycastHit hitInfo, interactRange, interactableLayer))
        {
            IInteractable interactableObject = hitInfo.collider.GetComponent<IInteractable>();
            interactableObject?.OnInteract();
        }
    }

    private void OnFire(object sender, EventArgs e)
    {
        // Implementar lógica de disparo
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
        BeginRoll();
        yield return ExecuteRoll();
        EndRoll();
    }

    private void BeginRoll()
    {
        isRolling = true;
        isInvincible = true;
    }

    private IEnumerator ExecuteRoll()
    {
        Vector3 rollDirection = transform.forward;
        float rollDuration = 0f; // Ajustar conforme necessário
        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            characterController.Move(rollSpeed * Time.deltaTime * rollDirection);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void EndRoll()
    {
        isRolling = false;
        StartCoroutine(PostRollInvincibility());
    }

    private IEnumerator PostRollInvincibility()
    {
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
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

    public bool IsWalking() => isWalking;

    public bool IsInvincible() => isInvincible;
}
