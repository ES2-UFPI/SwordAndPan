using System; // Importa o namespace System para usar tipos básicos como EventHandler e EventArgs
using System.Collections; // Importa o namespace System.Collections, necessário para coleções não genéricas (não usado diretamente aqui)
using System.Collections.Generic; // Importa o namespace System.Collections.Generic para coleções genéricas (não usado diretamente aqui)
using UnityEngine; // Importa o namespace UnityEngine para acessar as funcionalidades do Unity
using UnityEngine.InputSystem; // Importa o namespace UnityEngine.InputSystem para usar o novo sistema de entrada do Unity

public class GameInput : MonoBehaviour
{
    // Evento que será disparado quando a ação de interagir for realizada
    public event EventHandler OnInteractAction;

    // Referência para as ações de entrada do jogador, definida pela classe PlayerInputActions gerada automaticamente
    private PlayerInputActions playerInputActions;

    // Método chamado quando o script é inicializado
    private void Awake()
    {
        // Instancia as ações de entrada do jogador e as habilita
        playerInputActions = new PlayerInputActions(); // Cria uma nova instância de PlayerInputActions
        playerInputActions.Player.Enable(); // Habilita as ações de entrada definidas na seção Player

        // Adiciona um handler para o evento "performed" da ação "Interact"
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    // Método chamado quando a ação de interagir é realizada
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // Dispara o evento OnInteractAction, se houver algum inscrito
        OnInteractAction?.Invoke(this, EventArgs.Empty);
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
