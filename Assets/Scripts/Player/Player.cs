using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float gravity = -9.81f; // Gravidade aplicada ao jogador
    [SerializeField] private float gravityMulti = 2f; // Fator multiplicador para a gravidade
    [SerializeField] private float movementSpeed = 5f; // Velocidade de movimento do jogador
    private Vector3 playerVelocity; // Vetor de velocidade do jogador
    private CharacterController characterController;
    [SerializeField] private GameInput gameInput; // Referência ao script de entrada do jogador

    // Variável que indica se o jogador está andando
    private bool isWalking;

    private void Awake()
    {
        // Inicializa o CharacterController ao qual este script está anexado
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Gerencia o movimento do jogador
        HandleMovement();
        // Aplica a gravidade ao jogador
        ApplyGravity();
    }

    private void HandleMovement()
    {
        // Obtém o vetor de entrada normalizado do script de entrada do jogador
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Converte o vetor de entrada em um vetor de direção tridimensional
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        
        // Rotaciona o vetor de direção para corresponder à perspectiva isométrica
        movementDirection = Quaternion.Euler(0, 45f, 0) * movementDirection;

        // Calcula a velocidade de movimento horizontal com base na direção e na velocidade de movimento
        Vector3 horizontalVelocity = movementDirection * movementSpeed;

        // Atualiza a componente horizontal da velocidade do jogador
        playerVelocity.x = horizontalVelocity.x;
        playerVelocity.z = horizontalVelocity.z;

        // Move o jogador usando o CharacterController
        characterController.Move(playerVelocity * Time.deltaTime);

        // Verifica se o jogador está andando com base na direção de movimento
        isWalking = movementDirection != Vector3.zero;

        // Velocidade de rotação do jogador
        float rotateSpeed = 10f;

        // Rotaciona o jogador na direção de movimento usando Slerp (interpolação esférica)
        if (movementDirection != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
        }
    }

    private void ApplyGravity()
    {
        // Verifica se o jogador está no chão
        if (characterController.isGrounded)
        {
            // Reseta a velocidade vertical se estiver no chão
            playerVelocity.y = -1.0f; // Valor negativo para garantir que o jogador fique no chão
            Debug.Log("Player is grounded.");
        }
        else
        {
            // Aplica gravidade
            playerVelocity.y += gravityMulti * gravity * Time.deltaTime;
            Debug.Log("Applying gravity: " + playerVelocity.y);
        }
    }
}
