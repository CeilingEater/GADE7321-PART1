using UnityEngine;

public class InAirState : PlayerBaseState
{
    public override void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator)
    {
        animator.PlayTargetAnimation("Falling", false, false);
    }
    
    public override void UpdateState(PlayerManager player)
    {
        if (player.GetComponent<PlayerMovement>().isGrounded)
        {
            player.SwitchState(player.groundedState);
        }
    }
    
    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        movement.inAirTimer += Time.deltaTime;
        movement._playerRb.AddForce(Vector3.down * movement.fallingVelocity * movement.inAirTimer, ForceMode.Acceleration );
        
        
        movement.HandleMovement();
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}
