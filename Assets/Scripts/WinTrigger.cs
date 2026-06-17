using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Won!");
            PlayerStats.instance.WinLevel();
            SceneManager.LoadScene("MainMenu");
            this.enabled = false;
        }
    }
}