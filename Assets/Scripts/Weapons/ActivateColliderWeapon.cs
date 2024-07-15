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
        weaponCollider.enabled = true;
        StartCoroutine(DeactivateColliderAfterDelay(2f));
    }

    private IEnumerator DeactivateColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        weaponCollider.enabled = false;
    }
}
