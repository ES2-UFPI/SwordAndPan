using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 100;
    [SerializeField] protected GameObject dropObject;

    private bool isAlive = true;

    public virtual void TakeDamageEnemy(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
            isAlive = false;
        }
    }

    protected virtual void Die()
    {
        DropItem();
        Destroy(gameObject);
    }

    protected void DropItem()
    {
        if (dropObject != null)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }
    }

    public bool GetIsAlive(){
        return isAlive;
    }
}
