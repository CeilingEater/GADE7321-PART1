using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public Transform player; // The target for the AI

    [Header("Prefabs")]
    public GameObject turretPrefab;
    public GameObject scrappyPrefab;
    public GameObject heavyPrefab;

    public override EnemyAIBase CreateTurret(Vector3 pos)
    {
        GameObject obj = Instantiate(turretPrefab, pos, Quaternion.identity);
        TurretEnemy turret = obj.GetComponent<TurretEnemy>();
        
        turret.speed = 0; // Turrets don't move
        turret.size = new Vector3(1, 2, 1);
        turret.damage = 20;
        
        turret.Initialize(player);
        return turret;
    }

    public override EnemyAIBase CreateScrappy(Vector3 pos)
    {
        GameObject obj = Instantiate(scrappyPrefab, pos, Quaternion.identity);
        ScrappyEnemy scrappy = obj.GetComponent<ScrappyEnemy>();
        
        scrappy.speed = 8f; // Fast
        scrappy.size = new Vector3(0.5f, 0.5f, 0.5f); // Small
        scrappy.damage = 5;
        
        scrappy.Initialize(player);
        return scrappy;
    }

    public override EnemyAIBase CreateHeavy(Vector3 pos)
    {
        GameObject obj = Instantiate(heavyPrefab, pos, Quaternion.identity);
        HeavyEnemy heavy = obj.GetComponent<HeavyEnemy>();
        
        heavy.speed = 2f; // Slow
        heavy.size = new Vector3(2.5f, 2.5f, 2.5f); // Big
        heavy.damage = 50;
        
        heavy.Initialize(player);
        return heavy;
    }
}
