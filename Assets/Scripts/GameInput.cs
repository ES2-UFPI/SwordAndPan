using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions; // Referência para as ações de entrada do jogador

    // Método chamado quando o script é inicializado
    private void Awake()
    {
        // Instancia as ações de entrada do jogador e as habilita
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); // Habilita as ações do jogador
    }

    // Método para obter o vetor de movimento normalizado
    public Vector2 GetMovementVectorNormalized()
    {
        // Obtém o vetor de movimento do jogador a partir das ações de entrada
        Vector2 inputSystem = playerInputActions.Player.Move.ReadValue<Vector2>();
    
        // Normaliza o vetor de movimento
        inputSystem = inputSystem.normalized;

        // Retorna o vetor de movimento normalizado
        return inputSystem;
    }
}

