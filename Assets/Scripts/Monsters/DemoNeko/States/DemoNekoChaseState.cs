public class DemoNekoChaseState : DemoNekoBaseState
{
    public DemoNekoChaseState(DemoNeko demoNeko, DemoNekoStateManager stateManager) : base(demoNeko, stateManager)
    {
    }

    public override void Enter()
    {
        demoNeko.animator.SetTrigger("Chase");
    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
