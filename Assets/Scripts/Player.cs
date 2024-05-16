using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Velocidade de movimento do jogador
    [SerializeField] private float movSpeed;

    // Referência ao script de entrada do jogador
    [SerializeField] private GameInput gameInput;

    // Variável que indica se o jogador está andando
    private bool isWalking;

    // Método chamado a cada quadro
    private void Update()
    {
        // Obtém o vetor de entrada normalizado do script de entrada do jogador
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Converte o vetor de entrada em um vetor de direção tridimensional
        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Calcula a distância de movimento com base na velocidade de movimento e no tempo desde o último quadro
        float moveDistance = movSpeed * Time.deltaTime;

        // Altura e raio do jogador (usados para a detecção de colisão)
        float playerHeight = 2f;
        float playerRadius = .5f;

        // Verifica se o jogador pode se mover na direção desejada, usando um CapsuleCast para detecção de colisão
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, moveDistance);

        // Se o jogador não puder se mover na direção desejada...
        if (!canMove)
        {
            // Verifica se o jogador pode se mover apenas na direção X
            Vector3 moveDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            // Se o jogador puder se mover apenas na direção X, define a direção de movimento como X
            if (canMove)
            {
                movDir = moveDirX;
            }
            else
            {
                // Se o jogador não puder se mover na direção X, verifica se pode se mover apenas na direção Z
                Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirZ, moveDistance);

                // Se o jogador puder se mover apenas na direção Z, define a direção de movimento como Z
                if (canMove)
                {
                    movDir = movDirZ;
                }
                else
                {
                    // Não se move em direção nenhuma
                }
            }
        }

        // Se o jogador puder se mover, atualiza a posição do jogador na direção calculada
        if (canMove)
        {
            transform.position += moveDistance * movDir;
        }

        // Verifica se o jogador está andando com base na direção de movimento
        isWalking = movDir != Vector3.zero;

        // Velocidade de rotação do jogador
        float rotateSpeed = 10f;

        // Rotaciona o jogador na direção de movimento usando Slerp (interpolação esférica)
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    // Método público para verificar se o jogador está andando
    public bool IsWalking()
    {
        return isWalking;
    }
}
//Detalhe a criação do MoveDirX e MoveDirZ tem de ser normalizadas porque ao apertar os botões diagonais
//A velocidade do jogador fica mais lenta.