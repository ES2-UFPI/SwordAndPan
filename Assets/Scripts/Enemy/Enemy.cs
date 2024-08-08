using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Serialized fields
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected GameObject dropObject;
    [SerializeField] public float speed = 5f;
    [SerializeField] private float walkDistance = 10f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private FloatingHealthBar healthBar;

    // Private fields
    private Transform player;
    private Rigidbody rb;
    private PlayerStatus playerStatus;
    private Animator animator;

    private int currentHealth;
    private bool isWalking;
    private bool isAlive = true;
    private bool isAttacking;
    private bool canAttack = true;
    private bool isDamaged;

    // Public properties
    public bool IsAlive => isAlive;
    public bool IsWalking => isWalking;
    public bool IsAttacking => isAttacking;
    public bool IsDamaged => isDamaged;

    // Constants
    private const float ResetDamagedDelay = 0.2f;
    private const float AttackDelay = 0.5f;

    // Public Methods
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            isDamaged = true;
            StartCoroutine(ResetDamaged());
        }
    }

    // Protected Methods
    protected virtual void Die()
    {
        DropItem();
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                TakeDamage((int)weapon.GetDamageWeapon());
            }
        }
    }

    // Private Methods
    private void Start()
    {
        InitializeReferences();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
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
        if (player != null && IsAlive)
        {
            RotateTowardsPlayer();
            UpdateWalkingStatus();
            TryAttackPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void FixedUpdate()
    {
        if (player != null && IsAlive && isWalking)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }

    private void UpdateWalkingStatus()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isWalking = distanceToPlayer <= walkDistance;
    }

    private void TryAttackPlayer()
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
        yield return new WaitForSeconds(AttackDelay);
        isAttacking = false;
        playerStatus.TakeDamage(attackDamage);
        yield return new WaitForSeconds(attackCooldown - AttackDelay);
        canAttack = true;
    }

    private IEnumerator ResetDamaged()
    {
        yield return new WaitForSeconds(ResetDamagedDelay);
        isDamaged = false;
    }

    protected void DropItem()
    {
        if (dropObject != null)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }
    }
}
