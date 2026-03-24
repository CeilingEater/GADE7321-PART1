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
        CheckpointData newCheckpoint = new CheckpointData(playerTransform.position, currentScore, currentLives);
        _history.Push(newCheckpoint);
        Debug.Log("Saved Checkpoint");
    }

    public void LoadLastCheckpoint()
    {
        if (_history.IsEmpty())
        {
            Debug.LogWarning("no checkpoints to load");
            return;
        }
        
        CheckpointData lastCheckpoint = _history.Peek();
        
        playerTransform.position = lastCheckpoint.position;
        currentScore = lastCheckpoint.score;
        currentLives = lastCheckpoint.lives;
        
        Debug.Log("Loaded last checkpoint");
    }
}
