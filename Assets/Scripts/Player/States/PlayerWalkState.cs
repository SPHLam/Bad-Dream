using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(Player player, PlayerStateManager stateManager) : base(player, stateManager)
    {

    }
    public override void Enter()
    {
        player.animator.SetTrigger("Walk");
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
            player.animator.ResetTrigger("Walk");
            stateManager.SwitchState(stateManager.IdleState);
            return;
        }

        // RUN State
        if (player.playerMovement.inputHandler.IsRunning)
        {
            player.animator.ResetTrigger("Walk");
            stateManager.SwitchState(stateManager.RunState);
            return;
        }
    }

    public override void Exit()
    {

    }
}
