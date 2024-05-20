using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float movementSpeed = 5f; // Velocidade de movimento do jogador

    [Header("Interaction Settings")]
    [SerializeField] private GameInput gameInput; // Referência ao script de entrada do jogador
    [SerializeField] private Transform interactorSource; // Posição de origem do raio de interação
    [SerializeField] private float interactRange = 3f; // Alcance de interação do jogador
    [SerializeField] private LayerMask interactableLayer; // Camada dos objetos interativos

    // Variável que indica se o jogador está andando
    private bool isWalking;

    private void Start()
    {
        // Inscreve o método de interação do jogador ao evento de interação do GameInput
        gameInput.OnInteractAction += OnInteract;
    }

    private void OnInteract(object sender, EventArgs e)
    {
        Debug.Log("Player pressed Interact key.");

        // Dispara um raio da posição do jogador para verificar objetos interativos dentro do alcance
        Ray interactionRay = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(interactionRay, out RaycastHit hitInfo, interactRange, interactableLayer))
        {
            // Tenta obter o componente IInteractable do objeto atingido
            IInteractable interactableObject = hitInfo.collider.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                // Interage com o objeto atingido
                interactableObject.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Obtém o vetor de entrada normalizado do script de entrada do jogador
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Converte o vetor de entrada em um vetor de direção tridimensional
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        // Calcula a distância de movimento com base na velocidade de movimento e no tempo desde o último quadro
        float moveDistance = movementSpeed * Time.deltaTime;

        // Altura e raio do jogador (usados para a detecção de colisão)
        float playerHeight = 2f;
        float playerRadius = .5f;

        // Verifica se o jogador pode se mover na direção desejada, usando um CapsuleCast para detecção de colisão
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirection, moveDistance);

        // Se o jogador não puder se mover na direção desejada, tenta movimentar-se ao longo dos eixos X e Z separadamente
        if (!canMove)
        {
            Vector3 movementDirectionX = new Vector3(movementDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectionX, moveDistance);
            if (canMove)
            {
                movementDirection = movementDirectionX;
            }
            else
            {
                Vector3 movementDirectionZ = new Vector3(0, 0, movementDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectionZ, moveDistance);
                if (canMove)
                {
                    movementDirection = movementDirectionZ;
                }
                else
                {
                    movementDirection = Vector3.zero; // Não se move em direção nenhuma
                }
            }
        }

        // Se o jogador puder se mover, atualiza a posição do jogador na direção calculada
        if (canMove)
        {
            transform.position += moveDistance * movementDirection;
        }

        // Verifica se o jogador está andando com base na direção de movimento
        isWalking = movementDirection != Vector3.zero;

        // Velocidade de rotação do jogador
        float rotateSpeed = 10f;

        // Rotaciona o jogador na direção de movimento usando Slerp (interpolação esférica)
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed);
    }
}
