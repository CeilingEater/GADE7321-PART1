using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrol : MonoBehaviour
{
    private Waypoint _currentWaypoint; 
    private NavMeshAgent _agent;
    private bool _firstPathSet = false;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        FindClosestStartingWaypoint();
    }

    void Update()
    {
        if (_agent == null || !_agent.isOnNavMesh) return;
        
        if (!_firstPathSet)
        {
            if (_currentWaypoint != null)
            {
                _agent.SetDestination(_currentWaypoint.transform.position);
            }
            _firstPathSet = true;
            return;
        }
        
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        if (_currentWaypoint == null || GraphNetworkManager.instance == null) return;

    
        List<Waypoint> pathOptions = GraphNetworkManager.instance.gameGraph.GetConnectedVertices(_currentWaypoint);

        if (pathOptions == null || pathOptions.Count == 0) return;

        int randomIndex = Random.Range(0, pathOptions.Count);
        Waypoint nextTargetNode = pathOptions[randomIndex];

        if (nextTargetNode != null)
        {
            _currentWaypoint = nextTargetNode;
            _agent.SetDestination(_currentWaypoint.transform.position);
        }
    }

    private void FindClosestStartingWaypoint()
    {
        Waypoint[] allWaypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        Waypoint closest = null;

        foreach (Waypoint wp in allWaypoints)
        {
            float distance = Vector3.Distance(transform.position, wp.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = wp;
            }
        }
        _currentWaypoint = closest;
    }
}