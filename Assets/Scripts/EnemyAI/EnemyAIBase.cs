using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyAIBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed;
    public Vector3 size;
    public int damage;
    
    public Transform target;
    protected NavMeshAgent agent;
    
    [HideInInspector] public Waypoint currentGraphNode;
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

    protected void UpdatePatrol()
    {
        if (!canPatrol || currentGraphNode == null || agent == null) return;
        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            ChooseNextRandomNode();
        }
    }

    void ChooseNextRandomNode()
    {
        // FIXED: Changed currentWaypoint to currentGraphNode
        if (currentGraphNode == null || GraphNetworkManager.instance == null) return;

        // FIXED: Changed currentWaypoint to currentGraphNode
        List<Waypoint> pathOptions = GraphNetworkManager.instance.gameGraph.GetConnectedVertices(currentGraphNode);

        if (pathOptions == null || pathOptions.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, pathOptions.Count);
        Waypoint nextNode = pathOptions[randomIndex];

        if (nextNode != null)
        {
            // FIXED: Changed currentWaypoint to currentGraphNode
            currentGraphNode = nextNode;

            // FIXED: Changed currentWaypoint to currentGraphNode
            agent.SetDestination(currentGraphNode.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats.instance.LoseLife();
     
            if (this is ScrappyEnemy) 
            {
                Destroy(gameObject);
            }
        }
    }
}