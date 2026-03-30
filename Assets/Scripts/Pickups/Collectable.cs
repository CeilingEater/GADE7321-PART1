using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [Header("Base Settings")]
    public GameObject pickupEffect; //particle effect for part 2 n 3
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect();
            OnPickup();
        }
    }
    
    protected abstract void ApplyEffect();

    protected virtual void OnPickup()
    {
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}