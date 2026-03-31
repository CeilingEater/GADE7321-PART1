using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Won!");
            PlayerStats.instance.WinLevel();
            
            this.enabled = false;
        }
    }
}