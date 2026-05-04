using UnityEngine;

public class ScrappyEnemy : EnemyAIBase
{
    public override void Initialize(Transform playerTarget)
    {
        base.Initialize(playerTarget);
        canPatrol = true; 
    }

    private void Update()
    {
        UpdatePatrol();
    }
}
