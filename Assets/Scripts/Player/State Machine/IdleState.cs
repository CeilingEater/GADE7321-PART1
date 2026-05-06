using UnityEngine;

public class IdleState : PlayerBaseState
{
    public override void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator)
    {
        animator.UpdateAnimatorValues(0, 0, false);
        movement._playerRb.linearVelocity = Vector3.zero; //player was walking in place
    }

    public override void UpdateState(PlayerManager player)
    {
        InputManager input = player.GetComponent<InputManager>();
        
       
        if (input._moveAmount > 0)
        {
            if (input.sprintInput)
            {
                player.SwitchState(player.runningState);
            }
            else
            {
                player.SwitchState(player.walkingState);
            }
        }
    }

    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        movement.HandleFallingAndLanding();
        Vector3 stopVelocity = Vector3.zero;
        stopVelocity.y = movement._playerRb.linearVelocity.y;
        movement._playerRb.velocity = stopVelocity;
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}
