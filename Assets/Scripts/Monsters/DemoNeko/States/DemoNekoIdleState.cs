public class DemoNekoIdleState : DemoNekoBaseState
{
    public DemoNekoIdleState(DemoNeko demoNeko, DemoNekoStateManager stateManager) : base(demoNeko, stateManager)
    {
    }
    public override void Enter()
    {
        demoNeko.animator.SetTrigger("Idle");
    }

    public override void Update()
    {
        if (demoNeko.demoNekoMovement.IsAngry())
        {
            demoNeko.animator.ResetTrigger("Idle");
            stateManager.SwitchState(stateManager.TriggerState);
        }
        if (demoNeko.demoNekoMovement.CanFollowPlayer())
        {
            demoNeko.animator.ResetTrigger("Idle");
            stateManager.SwitchState(stateManager.FollowState);
        }
    }

    public override void Exit()
    {

    }
}
