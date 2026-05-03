using UnityEngine;

public class ConcreteEnemyFactory : EnemyFactoryBase
{
    public Transform player; 

    [Header("Prefabs")]
    public GameObject turretPrefab;
    public GameObject scrappyPrefab;
    public GameObject heavyPrefab;

    public override EnemyAIBase CreateTurret(Vector3 pos)
    {
        GameObject obj = Instantiate(turretPrefab, pos, Quaternion.identity);
        TurretEnemy turret = obj.GetComponent<TurretEnemy>();
        
        turret.speed = 0; 
        turret.size = new Vector3(1, 2, 1);
        turret.damage = 20;
        
        turret.Initialize(player);
        return turret;
    }

    public override EnemyAIBase CreateScrappy(Vector3 pos)
    {
        GameObject obj = Instantiate(scrappyPrefab, pos, Quaternion.identity);
        ScrappyEnemy scrappy = obj.GetComponent<ScrappyEnemy>();
        
        scrappy.speed = 8f; 
        scrappy.size = new Vector3(0.5f, 0.5f, 0.5f);
        scrappy.damage = 5;
        
        scrappy.Initialize(player);
        return scrappy;
    }

    public override EnemyAIBase CreateHeavy(Vector3 pos)
    {
        GameObject obj = Instantiate(heavyPrefab, pos, Quaternion.identity);
        HeavyEnemy heavy = obj.GetComponent<HeavyEnemy>();
        
        heavy.speed = 2f;
        heavy.size = new Vector3(2.5f, 2.5f, 2.5f); 
        heavy.damage = 50;
        
        heavy.Initialize(player);
        return heavy;
    }
}
