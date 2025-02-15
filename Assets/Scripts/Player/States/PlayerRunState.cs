using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(Player player, PlayerStateManager stateManager) : base(player, stateManager)
    {
    }

    public override void Enter()
    {
        player.animator.SetTrigger("Run");
    }

    public override void Update()
    {
        float moveInput = player.playerMovement.inputHandler.MoveInput;

        // JUMP State
        if (player.playerMovement.inputHandler.JumpPressed)
        {
            player.animator.ResetTrigger("Walk");
            stateManager.SwitchState(stateManager.JumpState);
            return;
        }

        // IDLE State
        if (Mathf.Abs(moveInput) < 0.1f)
        {
            player.animator.ResetTrigger("Run");
            stateManager.SwitchState(stateManager.IdleState);
            return;
        }

        // WALK State
        if (!player.playerMovement.inputHandler.IsRunning)
        {
            player.animator.ResetTrigger("Run");
            stateManager.SwitchState(stateManager.WalkState);
            return;
        }
    }
    public override void Exit()
    {

    }
}
