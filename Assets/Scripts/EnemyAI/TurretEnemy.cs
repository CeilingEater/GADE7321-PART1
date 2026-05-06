using UnityEngine;

public class TurretEnemy : EnemyAIBase
{
    public GameObject bulletPrefab;
    public Transform firePoint; 
    public float fireRate = 2f;
    private float _nextFireTime;

    public override void Initialize(Transform playerTarget)
    {
        base.Initialize(playerTarget);
        canPatrol = false; 
        
        _nextFireTime = Time.time + fireRate;
    }

    private void Update()
    {
        if (target == null) return;
        
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0;

        if (targetDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(targetDir);
        }
        
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log($"{gameObject.name} fired a bullet!"); 
        }
        else
        {
            Debug.LogError($"{gameObject.name} is trying to shoot but has no Bullet or FirePoint!");
        }
    }
}
