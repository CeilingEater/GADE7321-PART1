using UnityEngine;

public class TurretEnemy : EnemyAIBase
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Create an empty child on the turret to act as a nozzle
    public float fireRate = 2f;
    private float _nextFireTime;

    public override void Initialize(Transform playerTarget)
    {
        base.Initialize(playerTarget);
        canPatrol = false; // Turrets stay static
    }

    private void Update()
    {
        if (target == null) return;

        // 1. Rotate to face the player (Liminal/Eerie "Stalking" look)
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0; // Keep the turret upright
        transform.rotation = Quaternion.LookRotation(targetDir);

        // 2. Fire at intervals
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
            // This will tell us if the script running is the "empty" one
            Debug.LogError($"{gameObject.name} is trying to shoot but has no Bullet or FirePoint!");
        }
    }
}
