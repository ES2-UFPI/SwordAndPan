using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private GameInput gameInput; // Referência ao script de entrada do jogador
    [SerializeField] private Transform interactorSource; // Posição de origem do raio de interação
    [SerializeField] private float interactRange = 3f; // Alcance de interação do jogador
    [SerializeField] private LayerMask interactableLayer; // Camada dos objetos interativos
    // Start is called before the first frame update
    void Start()
    {
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
}
