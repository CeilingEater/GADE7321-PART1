using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private bool _hasBeenActivated = false;
    private CheckpointManager _checkpointManager;

    void Start()
    {
        _checkpointManager = FindFirstObjectByType<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenActivated)
        {
            _checkpointManager.SaveCheckpoint();
            _hasBeenActivated = true;
            
            Debug.Log("Activated checkpoint");
            //OnCheckPointActivated();
        }
    }

    /*void OnCheckPointActivated()                                    // for changing colour of checkpoint or smth like that to show player got it
    {
        GetComponent<Renderer>().material.color = Color.green;
    }*/
}
