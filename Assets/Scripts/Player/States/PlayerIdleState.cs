public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player player, PlayerStateManager stateManager) : base(player, stateManager) { }

    public override void Enter()
    {
        player.animator.SetTrigger("Idle");
    }

    public override void Update()
    {
        // JUMP State
        if (player.playerMovement.inputHandler.JumpPressed)
        {
            player.animator.ResetTrigger("Idle");
            stateManager.SwitchState(stateManager.JumpState);
            return;
        }

        if (player.playerMovement.inputHandler.MoveInput != 0)
        {
            player.animator.ResetTrigger("Idle");

            // RUN State
            if (player.playerMovement.inputHandler.IsRunning)
                stateManager.SwitchState(stateManager.RunState);

            // WALK State
            else
                stateManager.SwitchState(stateManager.WalkState);
        }
    }

    public override void Exit()
    {

    }
}
