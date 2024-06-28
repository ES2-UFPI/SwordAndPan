using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameInput gameInput;

    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "IsAttacking";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameInput.OnFireAction += OnFire;
    }

    private void OnFire(object sender, EventArgs e)
    {
        animator.SetBool(IS_ATTACKING, true);
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Tempo de duração do ataque
        animator.SetBool(IS_ATTACKING, false);
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
