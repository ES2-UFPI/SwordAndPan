using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractive : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f; // Alcance de interação
    [SerializeField] private GameObject dialogBoxPrefab; // Prefab da caixa de diálogo

    private GameObject player; // Referência ao jogador
    private GameObject dialogBoxInstance; // Instância da caixa de diálogo

    void Start()
    {
        // Encontrar o jogador no início (assumindo que o jogador tem a tag "Player")
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
        }
    }

    void Update()
    {
        // Verificar a distância entre o NPC e o jogador
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionRange)
        {
            // Se estiver dentro do alcance, exibir a caixa de diálogo
            if (dialogBoxInstance == null)
            {
                dialogBoxInstance = Instantiate(dialogBoxPrefab);
            }
        }
        else
        {
            // Se estiver fora do alcance, destruir a caixa de diálogo
            if (dialogBoxInstance != null)
            {
                Destroy(dialogBoxInstance);
                dialogBoxInstance = null;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Desenhar uma esfera para visualizar o alcance de interação no editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}