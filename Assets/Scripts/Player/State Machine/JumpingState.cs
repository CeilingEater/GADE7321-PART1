using UnityEngine;

public class JumpingState : PlayerBaseState
{
    private float jumpTimer;
    public override void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator)
    {
        movement.HandleJumping();
        jumpTimer = 0.5f;
    }

    public override void UpdateState(PlayerManager player)
    {
        jumpTimer -= Time.deltaTime;
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        
        if (movement.isGrounded && !movement.isJumping)
        {
            player.SwitchState(player.idleState);
        }
    }

    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        movement.HandleFallingAndLanding();
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}

