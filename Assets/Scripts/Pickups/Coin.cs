using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.instance.score += scoreValue;
            
            Debug.Log("Score: " + PlayerStats.instance.score);
            
            Destroy(gameObject);
        }
    }
}

