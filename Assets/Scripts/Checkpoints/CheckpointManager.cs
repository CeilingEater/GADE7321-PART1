using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private StackNode<CheckpointData> _history;

    public Transform playerTransform;
    public int currentScore;
    public int currentLives;

    void Start()
    {
        _history = new StackNode<CheckpointData>();
        SaveCheckpoint();
    }

    public void SaveCheckpoint()
    {
        int savedScore = PlayerStats.instance.score;
        int savedLives = PlayerStats.instance.currentLives;

        CheckpointData newCheckpoint = new CheckpointData(playerTransform.position, savedScore, savedLives);
        _history.Push(newCheckpoint);
    }

    public void LoadLastCheckpoint()
    {
        if (_history.IsEmpty()) return;
    
        CheckpointData lastCheckpoint = _history.Peek();
        
        playerTransform.position = lastCheckpoint.position;
        
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        PlayerMovement movement = playerTransform.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.inAirTimer = 0;
        }
    
        Debug.Log("Going to checkpoint: " + lastCheckpoint.position);
    }
    
    public void ResetStack()
    {
        _history = new StackNode<CheckpointData>();
        Debug.Log("stack cleared");
    }
}
