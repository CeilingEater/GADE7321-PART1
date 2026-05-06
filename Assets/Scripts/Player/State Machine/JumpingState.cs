using UnityEngine;

public class JumpingState : PlayerBaseState
{
    //private AnimatorManager animatorManager;
    //private float jumpTimer;
    private float cooldownTimer;
    public override void EnterState(PlayerManager player, PlayerMovement movement, AnimatorManager animator)
    {
        movement.HandleJumping();
        //jumpTimer = 0.5f;
        cooldownTimer = 0.2f;
    }

    public override void UpdateState(PlayerManager player)
    {
        cooldownTimer -= Time.deltaTime;
        //PlayerMovement movement = player.GetComponent<PlayerMovement>();

        if (cooldownTimer <= 0 && player._playerMovement.isGrounded)
        {
            player._animatorManager.animator.SetBool("isJumping", false);
            player.SwitchState(player.idleState);
        }
        
        /*if (movement.isGrounded && !movement.isJumping)
        {
            player.SwitchState(player.idleState);
        }*/
    }

    public override void FixedUpdateState(PlayerManager player, PlayerMovement movement)
    {
        movement.HandleAirMovement();
        movement.HandleFallingAndLanding();
    }

    public override void ExitState(PlayerManager player)
    {
        
    }
}

