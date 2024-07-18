using System;
using UnityEngine;

public class PlayerPickUpWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject weaponPrefab; // Prefab da arma que o jogador pode pegar
    [SerializeField] private Transform playerHand; // Transform onde a arma será posicionada ao ser pega
    private GameObject currentWeapon; // Referência à arma atualmente equipada

    public void OnInteract()
    {
        Debug.Log("Chegou aqui");
        PickUpWeapon();
    }
<<<<<<< Updated upstream
    public void OnInteract(object Player, EventArgs e)
=======
    public void OnInteract(object sender, EventArgs e)
>>>>>>> Stashed changes
    {
        Debug.Log("Chegou aqui");
        PickUpWeapon();
    }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    private void PickUpWeapon()
    {
        if (weaponPrefab != null)
        {
            // Verifica se já há uma arma equipada e a destrói
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            if (playerHand.childCount > 0)
            {
                Debug.Log("Destruindo a arma atual.");
                foreach (Transform child in playerHand)
                {
                    Destroy(child.gameObject);
                }
            }

            // Instancia a nova arma e a coloca na mão do jogador
            currentWeapon = Instantiate(weaponPrefab, playerHand.position, playerHand.rotation);
            currentWeapon.transform.SetParent(playerHand);
        }
        else
        {
            Debug.LogError("weaponPrefab não está atribuído.");
        }
    }
}
