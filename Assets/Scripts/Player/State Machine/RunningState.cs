using UnityEngine;

public class RunningState : PlayerBaseState
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

        if (!input.sprintInput && input._moveAmount > 0)
        {
            player.SwitchState(player.walkingState);
        }
    }

    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        movement.HandleAllMovement();
        player.GetComponent<AnimatorManager>().UpdateAnimatorValues(0, 1f, true);
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}

