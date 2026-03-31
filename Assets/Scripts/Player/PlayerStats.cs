using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxLives = 5;
    public int currentLives;
    public int score = 0;
     
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText; 
    
    public Vector3 startPosition;

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
        GetComponent<Animator>().SetBool("IsInteracting", false);
        Debug.Log("Resetting player to last checkpoint...");
        CheckpointManager cpManager = FindFirstObjectByType<CheckpointManager>();
        if (cpManager != null)
        {
            cpManager.LoadLastCheckpoint();
        }
    }

    public void UpdateUI()
    {
        if (livesText != null) 
            livesText.text = "Lives: " + currentLives;
    
        if (scoreText != null) 
            scoreText.text = "Score: " + score;
    }
    
    void ResetToStart()
    {
        GetComponent<Animator>().SetBool("IsInteracting", false);
        
        CheckpointManager cpManager = FindFirstObjectByType<CheckpointManager>();
        cpManager.ResetStack();

        
        currentLives = maxLives;
        score = 0;
        UpdateUI();

        transform.position = startPosition;
    
   
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null) rb.linearVelocity = Vector3.zero;
    }
}