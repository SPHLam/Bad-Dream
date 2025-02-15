using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(Player player, PlayerStateManager stateManager) : base(player, stateManager)
    {
    }

    public override void Enter()
    {
        player.animator.SetTrigger("Jump");
    }

    public override void Update()
    {
        float moveInput = player.playerMovement.inputHandler.MoveInput;

        if (player.playerMovement.isTouchingGround())
        {
            player.animator.ResetTrigger("Jump");

            // IDLE State
            if (Mathf.Abs(moveInput) < 0.1f)
            {
                stateManager.SwitchState(stateManager.IdleState);
                return;
            }

            // RUN State
            if (player.playerMovement.inputHandler.IsRunning)
            {
                stateManager.SwitchState(stateManager.RunState);
                return;
            }

            // WALK State
            else
            {
                stateManager.SwitchState(stateManager.WalkState);
                return;
            }

        }
    }

    public override void Exit()
    {
    }
}
