using System;
using System.Collections;
using UnityEngine;

public class ActivateColliderWeapon : MonoBehaviour
{
    private GameInput gameInput;
    [SerializeField] protected BoxCollider weaponCollider;

    private void Awake()
    {
        if (weaponCollider == null)
        {
            Debug.LogError("Weapon Collider not assigned.");
        }

        weaponCollider.enabled = false;
    }

    private void Start()
    {
        GameObject gameInputObject = GameObject.Find("GameInput");
        if (gameInputObject != null)
        {
            if (gameInputObject.TryGetComponent(out gameInput))
            {
                gameInput.OnFireAction += OnFire;
            }
            else
            {
                Debug.LogError("GameInput component not found on GameInput object.");
            }
        }
        else
        {
            Debug.LogError("GameInput object not found in the scene.");
        }
    }

    protected void OnFire(object sender, EventArgs e)
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
            StartCoroutine(DeactivateColliderAfterDelay(2f));
        }
        else
        {
            Debug.LogWarning("Weapon Collider is null.");
        }
    }

    private IEnumerator DeactivateColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (gameInput != null)
        {
            gameInput.OnFireAction -= OnFire; // Desinscreve do evento de ataque para evitar referências nulas
        }
    }
}
