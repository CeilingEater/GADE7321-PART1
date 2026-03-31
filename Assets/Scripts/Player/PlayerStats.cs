using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    SceneManager sceneManager;

    public int maxLives = 5;
    public int currentLives;
    public int score = 0;
     
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText; 
    
    public TextMeshProUGUI winText;
    public Button menuButton;
    
    public TextMeshProUGUI loseText;
    public Button restartButton;
    
    
    public Vector3 startPosition;

    void Awake() { instance = this; }

    void Start()
    {
        currentLives = maxLives;
        if (winText) winText.enabled = false;
        if (loseText) loseText.enabled = false; 
        if (menuButton) menuButton.enabled = false;
        if (restartButton) restartButton.enabled = false;
        UpdateUI();
    }
    
    public void WinGame()
    {
        if (winText != null) winText.enabled = true;
        menuButton.enabled = true;
        //Time.timeScale = 0; 
    }
    
    public void GameOver()
    {
        if (loseText != null) loseText.enabled = true;
        restartButton.enabled = true;
        //Time.timeScale = 0; 
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            Debug.Log("Game Over! Resetting everything.");
            ResetToStart();
        }
        else
        {
            Respawn(); 
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

        GameOver();
        currentLives = maxLives;
        score = 0;
        UpdateUI();

        transform.position = startPosition;
    
   
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null) rb.linearVelocity = Vector3.zero;
        
        Debug.Log("Resetting player to start.");
    }
}