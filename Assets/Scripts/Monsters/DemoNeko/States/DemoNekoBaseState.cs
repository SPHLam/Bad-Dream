public abstract class DemoNekoBaseState : IMonsterState
{
    protected DemoNeko demoNeko { get; private set; }
    protected DemoNekoStateManager stateManager { get; private set; }
    protected DemoNekoBaseState(DemoNeko demoNeko, DemoNekoStateManager stateManager)
    {
        this.demoNeko = demoNeko;
        this.stateManager = stateManager;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
