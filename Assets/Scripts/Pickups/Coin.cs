using UnityEngine;

public class Coin : Collectable
{
    public int scoreValue = 10;

    protected override void ApplyEffect()
    {
        PlayerStats.instance.score += scoreValue;
        Debug.Log("Score: " + PlayerStats.instance.score);
    }
}