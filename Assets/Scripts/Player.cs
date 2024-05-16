using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movSpeed; // Velocidade de movimento do jogador
    [SerializeField] private GameInput gameInput; // Referência ao script de input do jogo

    private bool isWalking; // Flag para indicar se o jogador está se movendo

    // Método chamado a cada quadro
    private void Update()
    {
        // Obtém o vetor de movimento normalizado do script de entrada do jogo
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Converte o vetor de movimento para um vetor tridimensional, mantendo a componente y como 0
        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Move o jogador na direção do vetor de movimento normalizado com a velocidade de movimento definida
        transform.position += movSpeed * Time.deltaTime * movDir;

        // Atualiza a flag isWalking com base no vetor de movimento
        isWalking = movDir != Vector3.zero;

        // Define a rotação do jogador para direção do movimento usando Slerp para suavizar a transição
        float rotateSpeed = 10f; // Velocidade de rotação
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    // Método público para verificar se o jogador está se movendo
    public bool IsWalking()
    {
        return isWalking; // Retorna o valor atual da flag isWalking
    }
}