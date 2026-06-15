using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

[System.Serializable]
public class EnemySpawnSetup
{
    public Transform spawnPoint;
    public Waypoint startingWaypoint;
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

        int selection = Random.Range(0, 3); 
        EnemyAIBase newEnemy = null;
        Vector3 pos = setup.spawnPoint.position;

        switch (selection)
        {
            case 0: newEnemy = factory.CreateScrappy(pos); break;
            case 1: newEnemy = factory.CreateHeavy(pos); break;
            case 2: newEnemy = factory.CreateTurret(pos); break;
        }

  
        if (newEnemy != null && selection != 2) 
        {
            newEnemy.currentGraphNode = setup.startingWaypoint;
        
           
            if (setup.startingWaypoint != null)
            {
                newEnemy.GetComponent<NavMeshAgent>().SetDestination(setup.startingWaypoint.transform.position);
            }
        }
    }

    /*void AssignWaypoints(EnemyAIBase enemy, List<Transform> waypoints)                          --- for linkedlist waypoints
    {
        if (waypoints == null || enemy == null) return;

        foreach (Transform wp in waypoints)
        {
            enemy.patrolWaypoints.Insert(wp);
        }
        
        if (waypoints.Count > 0)
        {
            var agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.SetDestination(waypoints[0].position);
        }
    }*/
}
