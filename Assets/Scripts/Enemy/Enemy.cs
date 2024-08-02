using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected int maxHealth = 100;
	[SerializeField] protected int health = 100;
	[SerializeField] protected GameObject dropObject;
	[SerializeField] public float speed = 5f; // Velocidade de movimento do inimigo
	[SerializeField] private float walkDistance = 10f; // Distância máxima para caminhar até o jogador
	[SerializeField] private float attackDistance = 2f; // Distância para atacar o jogador
	[SerializeField] private float attackDamage = 10f; // Dano do ataque do inimigo
	[SerializeField] private float attackCooldown = 5f; // Tempo de espera entre os ataques

	private Transform player; 
	private Rigidbody rb; 
	private Animator animator;
	private PlayerStatus playerStatus;
	[SerializeField] FloatingHealthBar healthBar;
	private bool isWalking;
	private bool isAlive = true;
	private bool isAttacking = false;
	private bool canAttack = true;
	private bool isDamaged = false;
	
	public bool GetIsAlive() => isAlive;
	public bool GetIsWalking() => isWalking;
	public bool IsAttacking() => isAttacking;
	public bool IsDamaged() => isDamaged;

	// Métodos Públicos
	public virtual void TakeDamageEnemy(int damage)
	{
		health -= damage;
		healthBar.UpdateHealthBar(health, maxHealth);
		Debug.Log(health);
		if (health <= 0)
		{
			Die();
			isAlive = false;
		}
		else
		{
			isDamaged = true;
			StartCoroutine(ResetDamaged());
		}
	}


	// Métodos Protegidos
	protected virtual void Die()
	{
		DropItem();
		Destroy(gameObject);
	}

	// Métodos Privados
	private void Start()
	{
		InitializeReferences();
		
		healthBar.UpdateHealthBar(health, maxHealth);
	}

	private void InitializeReferences()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody>();
		playerStatus = player.GetComponent<PlayerStatus>();
		healthBar = GetComponentInChildren<FloatingHealthBar>();
	}

	private void Update()
	{
		if (player != null && GetIsAlive())
		{
			RotateTowardsPlayer();
			CheckDistanceToPlayer();
			CheckAttackRange();
		}
	}

	private void RotateTowardsPlayer()
	{
		Vector3 direction = player.position - transform.position;
		direction.Normalize();

		if (direction != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}

	private void FixedUpdate()
	{
		if (player != null && GetIsAlive() && isWalking)
		{
			MoveTowardsPlayer();
		}
	}

	private void MoveTowardsPlayer()
	{
		Vector3 direction = player.position - transform.position;
		direction.Normalize();

		rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
	}

	private void CheckDistanceToPlayer()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, player.position);
		isWalking = distanceToPlayer <= walkDistance;
	}

	private void CheckAttackRange()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, player.position);
		if (distanceToPlayer <= attackDistance && canAttack)
		{
			StartCoroutine(AttackPlayer());
		}
	}

	private IEnumerator AttackPlayer()
	{
		isAttacking = true;
		canAttack = false;
		yield return new WaitForSeconds(0.5f); // Tempo de duração da animação de ataque
		isAttacking = false;
		playerStatus.TakeDamage(attackDamage);
		yield return new WaitForSeconds(attackCooldown - 0.5f); // Aguarda o restante do cooldown
		canAttack = true;
	}

	private IEnumerator ResetDamaged()
	{
		yield return new WaitForSeconds(0.2f); // Tempo de duração da animação de dano
		isDamaged = false;
	}

	protected void DropItem()
	{
		if (dropObject != null)
		{
			Instantiate(dropObject, transform.position, Quaternion.identity);
		}
	}

	// Método para tomar dano ao colidir com um objeto de dano
	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Weapon"))
		{
			TakeDamageEnemy((int)other.GetComponent<Weapon>().GetDamageWeapon());
		}
	}
}