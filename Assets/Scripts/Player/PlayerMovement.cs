using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    public PlayerInputHandler inputHandler;

    /* MOVE INFO */
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed = 7.5f;
    [SerializeField] private float _runSpeed = 15f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private LayerMask _groundLayer; // Layer for ground detection
    [SerializeField] private BoxCollider2D _boxCollider;
    private bool _isGrounded;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player.IsHiding() == false)
        {
            HandleMovement();
            HandleJump();
        }
    }

    private void HandleMovement()
    {
        float speed = inputHandler.IsRunning ? _runSpeed : _moveSpeed;
        _rb.velocity = new Vector2(inputHandler.MoveInput * speed, _rb.velocity.y);
    }

    private void HandleJump()
    {
        if (inputHandler.JumpPressed && isTouchingGround())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }

    public bool isTouchingGround()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 2.2f, _groundLayer);
        return _isGrounded;
    }
}
