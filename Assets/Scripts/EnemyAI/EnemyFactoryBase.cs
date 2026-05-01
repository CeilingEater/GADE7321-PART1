using UnityEngine;

public abstract class EnemyFactoryBase : MonoBehaviour
{
    public abstract EnemyAIBase CreateTurret(Vector3 pos);
    public abstract EnemyAIBase CreateScrappy(Vector3 pos);
    public abstract EnemyAIBase CreateHeavy(Vector3 pos);
}