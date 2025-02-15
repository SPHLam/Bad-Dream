using UnityEngine;

public class DemoNekoStateManager : MonoBehaviour
{
    private IMonsterState _currentState;
    private DemoNeko _demoNeko;
    public DemoNekoIdleState IdleState { get; private set; }
    public DemoNekoFollowState FollowState { get; private set; }
    public DemoNekoTriggerState TriggerState { get; private set; }
    public DemoNekoChaseState ChaseState { get; private set; }

    private void InitializeStates()
    {
        IdleState = new DemoNekoIdleState(_demoNeko, this);
        FollowState = new DemoNekoFollowState(_demoNeko, this);
        TriggerState = new DemoNekoTriggerState(_demoNeko, this);
        ChaseState = new DemoNekoChaseState(_demoNeko, this);
    }
    private void Awake()
    {
        _demoNeko = GetComponent<DemoNeko>();
        InitializeStates();
    }
    // Start is called before the first frame update
    void Start()
    {
        _currentState = IdleState;
        _currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != null)
            _currentState.Update();
    }

    public void SwitchState(IMonsterState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
