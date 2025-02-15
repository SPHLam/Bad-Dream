public abstract class PlayerBaseState : IPlayerState
{
    protected Player player { get; private set; }
    protected PlayerStateManager stateManager { get; private set; }

    protected PlayerBaseState(Player player, PlayerStateManager stateManager)
    {
        this.player = player;
        this.stateManager = stateManager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
