using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float threshold;
    public CheckpointManager checkpointManager;

    private void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            
            PlayerStats.instance.LoseLife();
        }
    }
}
