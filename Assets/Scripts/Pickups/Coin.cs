using UnityEngine;

public class Coin : Collectable
{
    public int scoreValue = 10;

    protected override void ApplyEffect()
    {
        PlayerStats.instance.score += scoreValue;
        PlayerStats.instance.UpdateUI();
        Debug.Log("Score: " + PlayerStats.instance.score);
    }
}