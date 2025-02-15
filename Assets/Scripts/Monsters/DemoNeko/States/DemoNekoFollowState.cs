public class DemoNekoFollowState : DemoNekoBaseState
{
    public DemoNekoFollowState(DemoNeko demoNeko, DemoNekoStateManager stateManager) : base(demoNeko, stateManager)
    {
    }

    public override void Enter()
    {
        demoNeko.animator.SetTrigger("Follow");
    }

    public override void Update()
    {
        if (!demoNeko.demoNekoMovement.CanFollowPlayer())
        {
            demoNeko.animator.ResetTrigger("Follow");
            stateManager.SwitchState(stateManager.IdleState);
        }
    }

    public override void Exit()
    {

    }
}
