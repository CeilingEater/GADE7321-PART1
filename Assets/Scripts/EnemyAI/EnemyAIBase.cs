using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyAIBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed;
    public Vector3 size;
    public int damage;
    
    protected NavMeshAgent agent;
    // Using your custom Linked List!
    public LinkedListNode<Transform> patrolWaypoints = new LinkedListNode<Transform>();
    protected int currentWaypointIndex = 0;
    protected bool canPatrol = false;

    public virtual void Initialize(Transform playerTarget)
    {
        this.transform.localScale = size;
        agent = GetComponent<NavMeshAgent>();
        
        if (agent != null)
        {
            agent.speed = speed;
        }
    }

    // This will handle the movement logic for Scrappy and Heavy
    protected void UpdatePatrol()
    {
        if (!canPatrol || patrolWaypoints.Size == 0 || agent == null) return;

        // Check if we've arrived at the current waypoint
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Size;
            agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        }
    }
}