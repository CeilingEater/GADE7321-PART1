using UnityEngine;

public class GroundedState : PlayerBaseState
{
    public override void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator)
    {
        
    }
    
    public override void UpdateState(PlayerManager player)
    {
        if (!player.GetComponent<PlayerMovement>().isGrounded)
        {
            player.SwitchState(player.inAirState);
        }
    }
    
    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        movement = player.GetComponent<PlayerMovement>();
        
        movement.HandleMovement();
        movement.HandleRotation();
        movement.HandleFallingAndLanding();
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}
