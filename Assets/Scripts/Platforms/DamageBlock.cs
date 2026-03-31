using System;
using UnityEngine;

public class HazardTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            Debug.Log("Hit a hazard!");
            
            if (PlayerStats.instance != null)
            {
                PlayerStats.instance.LoseLife();
            }
        }
    }
    
}