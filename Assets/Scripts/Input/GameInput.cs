using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction; // Evento de interação
    public event EventHandler OnFireAction; // Evento de ataque
    public event EventHandler OnRollAction; // Evento de rolamento

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); // Habilita as ações de entrada definidas na seção Player

        playerInputActions.Player.Interact.performed += Interact_performed; // Liga a ação de interação
        playerInputActions.Player.Attack.performed += Fire_performed; // Liga a ação de ataque
        playerInputActions.Player.Roll.performed += Roll_performed; // Liga a ação de rolamento
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty); // Dispara o evento de interação
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        OnFireAction?.Invoke(this, EventArgs.Empty); // Dispara o evento de ataque
    }

    private void Roll_performed(InputAction.CallbackContext obj)
    {
        OnRollAction?.Invoke(this, EventArgs.Empty); // Dispara o evento de rolamento
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputSystem = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputSystem.normalized;
    }
}
