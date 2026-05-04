using UnityEngine;

public abstract class PlayerBaseState 
{
    public abstract void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator);
    public abstract void ExitState(PlayerManager player);
    public abstract void UpdateState(PlayerManager player);
    public abstract void FixedUpdateState(PlayerManager player, PlayerMovement movement);
}
