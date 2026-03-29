using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxLives = 5;
    public int currentLives;
    public int score = 0;

    public TextMeshProUGUI livesText; 

    void Awake() { instance = this; }

    void Start()
    {
        currentLives = maxLives;
        UpdateUI();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateUI();

        if (currentLives > 0)
        {
            Respawn();
        }
        else
        {
            Debug.Log("Game Over!");
            // sceneloader 
        }
    }

    public void GainLife()
    {
        currentLives++;
        UpdateUI();
    }

    void Respawn()
    {
        // This is where you call your partner's Stack system
        // Example: transform.position = CheckpointManager.instance.GetLastCheckpoint();
        Debug.Log("Resetting player to last checkpoint...");
    }

    void UpdateUI()
    {
        if (livesText != null) livesText.text = "Lives: " + currentLives;
    }
}