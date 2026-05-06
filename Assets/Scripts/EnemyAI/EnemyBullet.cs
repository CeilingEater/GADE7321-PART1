using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Clean up bullet if it misses
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.instance.LoseLife();
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            // Destroy bullet if it hits a wall/floor
            Destroy(gameObject);
        }
    }
}
