using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyAIBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed;
    public Vector3 size;
    public int damage;
    
    [HideInInspector] public Transform target; // for nav mesh

    public virtual void Initialize(Transform playerTarget)
    {
        this.target = playerTarget;
        this.transform.localScale = size;
        
        // Setup NavMesh if it exists
        var nav = GetComponent<NavMeshAgent>();
        if (nav != null) nav.speed = speed;
    }
}