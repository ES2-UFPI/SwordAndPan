using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private bool isStopped = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomWaypoint();
    }

    void Update()
    {
        animator.SetBool("IsWalking", !isStopped && agent.remainingDistance > agent.stoppingDistance);
        if (!isStopped && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isStopped = true;
            OrderDish();
        }
    }

    void MoveToRandomWaypoint()
    {
        int randomIndex = Random.Range(0, waypoints.Length);
        Vector3 targetPosition = waypoints[randomIndex].position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogError("Waypoint " + targetPosition + " is not on the NavMesh");
        }
    }

    void OrderDish()
    {
        // Simulate ordering a dish
        Debug.Log("Ordering dish...");
        Invoke("Disappear", 5f); // Simulate a 5-second delay for ordering and delivery
    }

    void Disappear()
    {
        // Disable the NPC GameObject
        gameObject.SetActive(false);
    }
}