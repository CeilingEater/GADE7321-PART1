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
            // Use the Stack to go back one step
            Respawn(); 
        }
        else
        {
            Debug.Log("Game Over! Resetting everything.");
            ResetToStart();
        }
    }

    public void GainLife()
    {
        if (currentLives >= maxLives)
        {
            maxLives++;
            currentLives = maxLives;
            Debug.Log("Max Lives Increased to: " + maxLives);
        }
        else
        {
            currentLives++;
            Debug.Log("Life Replenished. Current: " + currentLives);
        }
    
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
    
    void ResetToStart()
    {
        // 1. Clear the Stack completely
        CheckpointManager cpManager = FindFirstObjectByType<CheckpointManager>();
        cpManager.ResetStack(); // We need to add this method to CheckpointManager

        // 2. Reset Stats
        currentLives = maxLives;
        score = 0;
        UpdateUI();

        // 3. Move player to a designated "Spawn Point" (e.g., 0,0,0)
        transform.position = new Vector3(0, 5, 0); 
    }
}