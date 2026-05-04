using UnityEngine;

public class HeavyEnemy : EnemyAIBase
{
    public override void Initialize(Transform playerTarget)
    {
        base.Initialize(playerTarget);
        canPatrol = true; // Heavy enemies patrol
    }

    private void Update()
    {
        UpdatePatrol();
    }
}
