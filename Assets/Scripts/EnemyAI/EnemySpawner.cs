using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactoryBase factory; 
    public List<Transform> spawnPoints;

    [Header("Waypoint Groups")]
    public List<Transform> scrappyWaypoints;
    public List<Transform> heavyWaypoints;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SpawnEnemy("scrappy");
        if (Input.GetKeyDown(KeyCode.H)) SpawnEnemy("heavy");
        if (Input.GetKeyDown(KeyCode.T)) SpawnEnemy("turret");
    }

    void SpawnEnemy(string type)
    {
        if (spawnPoints.Count == 0) return;
        
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        EnemyAIBase newEnemy = null;

        if (type == "scrappy")
        {
            newEnemy = factory.CreateScrappy(pos);
            AssignWaypoints(newEnemy, scrappyWaypoints);
        }
        else if (type == "heavy")
        {
            newEnemy = factory.CreateHeavy(pos);
            AssignWaypoints(newEnemy, heavyWaypoints);
        }
        else if (type == "turret")
        {
            newEnemy = factory.CreateTurret(pos);
        }
    }

    // Helper to fill the Linked List
    void AssignWaypoints(EnemyAIBase enemy, List<Transform> waypoints)
    {
        foreach (Transform wp in waypoints)
        {
            enemy.patrolWaypoints.Insert(wp);
        }
        
        // Give them their first destination immediately
        if (waypoints.Count > 0)
        {
            enemy.GetComponent<NavMeshAgent>().SetDestination(waypoints[0].position);
        }
    }
}
