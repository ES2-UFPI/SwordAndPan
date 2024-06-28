using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    // Eventos que serão disparados quando as ações de interagir e atirar forem realizadas
    public event EventHandler OnInteractAction;
    public event EventHandler OnFireAction;

    // Referência para as ações de entrada do jogador, definida pela classe PlayerInputActions gerada automaticamente
    private PlayerInputActions playerInputActions;

    // Método chamado quando o script é inicializado
    private void Awake()
    {
        // Instancia as ações de entrada do jogador e as habilita
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); // Habilita as ações de entrada definidas na seção Player

        // Adiciona handlers para os eventos "performed" das ações "Interact" e "Attack"
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Attack.performed += Fire_performed;
    }

    // Método chamado quando a ação de interagir é realizada
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        // Dispara o evento OnInteractAction, se houver algum inscrito
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // Método chamado quando a ação de atirar é realizada
    private void Fire_performed(InputAction.CallbackContext obj)
    {
        // Dispara o evento OnFireAction, se houver algum inscrito
        OnFireAction?.Invoke(this, EventArgs.Empty);
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
