using UnityEngine;

public class WinDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Goal Reached!");
            
            if (PlayerStats.instance != null)
            {
                PlayerStats.instance.WinGame();
            }
            else
            {
                Debug.LogError("PlayerStats instance not found! Is the script on the Player?");
            }
        }
    }
}