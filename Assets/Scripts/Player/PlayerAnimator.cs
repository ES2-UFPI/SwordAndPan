// File: Assets/Scripts/PlayerAnimator.cs
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private Player player; // Referência ao jogador
	[SerializeField] private GameInput gameInput; // Referência à entrada do jogo

	private static readonly int IsWalking = Animator.StringToHash("IsWalking"); // Nome do parâmetro de animação de caminhar
	private static readonly int IsAttacking = Animator.StringToHash("IsAttacking"); // Nome do parâmetro de animação de ataque
	private static readonly int IsRolling = Animator.StringToHash("IsRolling"); // Nome do parâmetro de animação de rolamento
	private static readonly int IsDamage = Animator.StringToHash("IsDamage"); // Nome do parâmetro de animação de dano

	private Animator animator; // Referência ao componente Animator
	private PlayerStatus playerStatus; // Referência ao componente PlayerStatus

	private void Awake()
	{
		animator = GetComponent<Animator>();
		playerStatus = player.GetComponent<PlayerStatus>();
	}

	private void Start()
	{
		RegisterEventHandlers();
	}

	private void RegisterEventHandlers()
	{
		gameInput.OnFireAction += OnFire; // Subscreve ao evento de ataque
		gameInput.OnRollAction += OnRoll; // Subscreve ao evento de rolamento
	}

	private void OnFire(object sender, EventArgs e)
	{
		TriggerAnimation(IsAttacking, ResetAttack, 0.5f);
	}

	private void OnRoll(object sender, EventArgs e)
	{
		TriggerAnimation(IsRolling, ResetRoll, 0.2f);
	}

	private void TriggerAnimation(int parameter, Action resetAction, float delay)
	{
		animator.SetBool(parameter, true); // Ativa a animação
		StartCoroutine(ResetAnimation(parameter, resetAction, delay));
	}

	private IEnumerator ResetAnimation(int parameter, Action resetAction, float delay)
	{
		yield return new WaitForSeconds(delay); // Tempo de duração da animação
		animator.SetBool(parameter, false); // Desativa a animação
		resetAction?.Invoke();
	}

	private void ResetAttack()
	{
		// Outras lógicas relacionadas ao reset de ataque podem ser adicionadas aqui.
	}

	private void ResetRoll()
	{
		// Outras lógicas relacionadas ao reset de rolamento podem ser adicionadas aqui.
	}

	private void Update()
	{
		UpdateWalkingAnimation();
		CheckForDamage();
	}

	private void UpdateWalkingAnimation()
	{
		animator.SetBool(IsWalking, player.IsWalking());
	}

	private void CheckForDamage()
	{
		if (playerStatus.GetIsDamaged())
		{
			animator.SetTrigger(IsDamage);
		}
	}
}
