public class DemoNekoTriggerState : DemoNekoBaseState
{
    public DemoNekoTriggerState(DemoNeko demoNeko, DemoNekoStateManager stateManager) : base(demoNeko, stateManager)
    {
    }

    public override void Enter()
    {
        demoNeko.animator.SetTrigger("Angry");
    }

    public override void Update()
    {
        if (demoNeko.demoNekoMovement.IsChasingMode())
        {
            demoNeko.animator.ResetTrigger("Angry");
            stateManager.SwitchState(stateManager.ChaseState);
        }
    }

    public override void Exit()
    {

    }
}
