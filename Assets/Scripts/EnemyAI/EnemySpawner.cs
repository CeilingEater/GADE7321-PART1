using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

[System.Serializable]
public class EnemySpawnSetup
{
    public Transform spawnPoint;
    public List<Transform> waypoints;
    // We removed 'enemyType' from here so any enemy can spawn here!
}
public class EnemySpawner : MonoBehaviour
{
    public EnemyFactoryBase factory; 
    public List<EnemySpawnSetup> levelSpawnSetups;

    private void Start()
    {
        foreach (EnemySpawnSetup setup in levelSpawnSetups)
        {
            SpawnRandomEnemyAtSetup(setup);
        }
    }

    void SpawnRandomEnemyAtSetup(EnemySpawnSetup setup)
    {
        if (setup.spawnPoint == null) return;

        // 1. Pick a random number between 0 and 2
        int selection = Random.Range(0, 3); 
        EnemyAIBase newEnemy = null;
        Vector3 pos = setup.spawnPoint.position;

        // 2. Use the factory based on the random selection
        switch (selection)
        {
            case 0:
                newEnemy = factory.CreateScrappy(pos);
                AssignWaypoints(newEnemy, setup.waypoints);
                break;
            case 1:
                newEnemy = factory.CreateHeavy(pos);
                AssignWaypoints(newEnemy, setup.waypoints);
                break;
            case 2:
                newEnemy = factory.CreateTurret(pos);
                // Turrets are static, so they ignore the waypoints list
                break;
        }
    }

    void AssignWaypoints(EnemyAIBase enemy, List<Transform> waypoints)
    {
        if (waypoints == null || enemy == null) return;

        foreach (Transform wp in waypoints)
        {
            enemy.patrolWaypoints.Insert(wp);
        }
        
        // Ensure the NavMeshAgent starts moving toward the local path immediately
        if (waypoints.Count > 0)
        {
            var agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.SetDestination(waypoints[0].position);
        }
    }
}
