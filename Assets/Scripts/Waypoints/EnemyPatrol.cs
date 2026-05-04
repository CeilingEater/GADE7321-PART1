using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    // Using your custom Linked List class
    private LinkedListNode<Transform> _waypointList;
    
    private int _currPointIndex = 0;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _waypointList = new LinkedListNode<Transform>();

        // 1. Find all objects tagged "waypoint"
        GameObject[] wpObjects = GameObject.FindGameObjectsWithTag("waypoint");

        // 2. Insert them into your custom Linked List
        foreach (GameObject go in wpObjects)
        {
            _waypointList.Insert(go.transform);
        }

        // 3. Start the patrol
        if (_waypointList.Size > 0)
        {
            _agent.SetDestination(_waypointList[0].position);
        }
    }

    void Update()
    {
        // Check if we have reached the destination
        // agent.pathPending ensures we don't skip logic while the AI is still "thinking"
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        if (_waypointList.Size == 0) return;

        // Increment index and loop back using your Size property
        _currPointIndex = (_currPointIndex + 1) % _waypointList.Size;

        // Use your custom indexer [int index] to get the transform
        Transform nextTarget = _waypointList[_currPointIndex];
        
        _agent.SetDestination(nextTarget.position);
    }
}