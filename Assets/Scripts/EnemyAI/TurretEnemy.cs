using UnityEngine;

public class TurretEnemy : EnemyAIBase
{
    public override void Initialize(Transform playerTarget)
    {
        base.Initialize(playerTarget);
        canPatrol = false; // Turrets stay static
    }
    // We don't call UpdatePatrol here, so it never moves!
}
