using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactoryBase factory; 
    
    public List<Transform> spawnPoints;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnEnemy("scrappy");
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnEnemy("heavy");
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnEnemy("turret");
        }
    }

    void SpawnEnemy(string type)
    {
        if (spawnPoints.Count == 0)
        {
            return;
        }
        
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Vector3 pos = spawnPoints[randomIndex].position;

        if (type == "scrappy")
        {
            factory.CreateScrappy(pos);
        }

        if (type == "heavy")
        {
            factory.CreateHeavy(pos);
        }

        if (type == "turret")
        {
            factory.CreateTurret(pos);
        }
    }
}