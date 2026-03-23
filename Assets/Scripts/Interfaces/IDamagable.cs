using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float amount, GameObject source);
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool IsDead { get; }
}
