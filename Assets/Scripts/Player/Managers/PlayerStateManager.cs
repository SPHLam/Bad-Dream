using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState _currentState;
    private Player _player;
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }

    private void InitializeStates()
    {
        IdleState = new PlayerIdleState(_player, this);
        WalkState = new PlayerWalkState(_player, this);
        RunState = new PlayerRunState(_player, this);
        JumpState = new PlayerJumpState(_player, this);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        InitializeStates();
    }

    void Start()
    {
        _currentState = IdleState;
        _currentState.Enter();
    }

    void Update()
    {
        if (_currentState != null)
            _currentState.Update();
    }

    public void SwitchState(IPlayerState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
