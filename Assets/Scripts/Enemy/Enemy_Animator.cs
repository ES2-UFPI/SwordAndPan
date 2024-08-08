using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DOTween namespace

public class Enemy_Animator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking"; // Nome do parâmetro de animação de caminhar
    private const string IS_ATTACKING = "attack"; // Nome do parâmetro de animação de ataque
    private const string IS_DAMAGED = "damaged"; // Nome do parâmetro de animação de dano

    private Animator animator; // Referência ao componente Animator
    private Enemy enemy;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, enemy.IsWalking);
        Attack();
        Damaged();
    }

    private void Attack()
    {
        if (enemy.IsAttacking)
        {
            animator.SetTrigger(IS_ATTACKING);
        }
    }

    private void Damaged()
    {
        if (enemy.IsDamaged)
        {
            animator.SetTrigger(IS_DAMAGED);
        }
    }
}