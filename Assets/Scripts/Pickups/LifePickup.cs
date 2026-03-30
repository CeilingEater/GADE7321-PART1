using UnityEngine;

public class LifePickup : Collectable
{
    protected override void ApplyEffect()
    {
        if (PlayerStats.instance != null)
        {
            PlayerStats.instance.GainLife();
        }
    }
}