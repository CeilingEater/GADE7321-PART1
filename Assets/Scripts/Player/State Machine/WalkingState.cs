using UnityEngine;

public class WalkingState : PlayerBaseState
{
    public override void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator)
    {
        
    }

    public override void UpdateState(PlayerManager player)
    {
        InputManager input = player.GetComponent<InputManager>();

        if (input._moveAmount <= 0)
        { 
            player.SwitchState(player.idleState);
        }

        if (input.sprintInput)
        {
            player.SwitchState(player.runningState);
        }
    }

    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        InputManager input = player.GetComponent<InputManager>();
        movement.HandleAllMovement();
        player.GetComponent<AnimatorManager>().UpdateAnimatorValues(0, input._moveAmount, false);
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}
