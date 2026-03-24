using UnityEngine;

[System.Serializable]
public class CheckpointData //has all da data to save into the checkpoint
{
    public Vector3 position;
    public int score;
    public int lives;

    public CheckpointData(Vector3 pos, int s, int l)
    {
        position = pos;
        score = s;
        lives = l;
    }

}
