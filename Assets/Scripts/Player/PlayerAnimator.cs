using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player; // Referência ao jogador
    [SerializeField] private GameInput gameInput; // Referência à entrada do jogo

    private const string IS_WALKING = "IsWalking"; // Nome do parâmetro de animação de caminhar
    private const string IS_ATTACKING = "IsAttacking"; // Nome do parâmetro de animação de ataque
    private const string IS_ROLLING = "IsRolling"; // Nome do parâmetro de animação de rolamento
    private Animator animator; // Referência ao componente Animator

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameInput.OnFireAction += OnFire; // Subscreve ao evento de ataque
        gameInput.OnRollAction += OnRoll; // Subscreve ao evento de rolamento
    }

    private void OnFire(object sender, EventArgs e)
    {
        animator.SetBool(IS_ATTACKING, true); // Ativa a animação de ataque
        StartCoroutine(ResetAttack());
    }

    private void OnRoll(object sender, EventArgs e)
    {
        animator.SetBool(IS_ROLLING, true); // Ativa a animação de rolamento
        StartCoroutine(ResetRoll());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Tempo de duração do ataque
        animator.SetBool(IS_ATTACKING, false); // Desativa a animação de ataque
    }

    private IEnumerator ResetRoll()
    {
        yield return new WaitForSeconds(0.2f); // Tempo de duração do rolamento
        animator.SetBool(IS_ROLLING, false); // Desativa a animação de rolamento
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking()); // Atualiza a animação de caminhar
    }
}
