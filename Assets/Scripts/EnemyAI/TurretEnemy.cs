using UnityEngine;

public class TurretEnemy : EnemyAIBase
{
    public override void Initialize(Transform target)
    {
        this.speed = 0f; //no movement
        base.Initialize(target);
    }

    //add code to rotate towards player and shoot projectiles
    private void Start()
    {
        Debug.Log("Turret has spawned");
    }
}
