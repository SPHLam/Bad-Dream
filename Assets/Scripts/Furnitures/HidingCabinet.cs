using UnityEngine;

public class HidingCabinet : MonoBehaviour
{
    private bool _isPlayerColliding;
    private Player _player;
    private Animator _animator;
    private System.Action<GameObject> _returnToPool;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            _isPlayerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            _isPlayerColliding = false;
        }
    }

    public void Initialize(System.Action<GameObject> poolReturn)
    {
        _returnToPool = poolReturn;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
            Debug.Log("Player is null");
    }

    private void Update()
    {
        HandleOpeningClosingLocker();
    }

    private void HandleOpeningClosingLocker()
    {
        if (_isPlayerColliding && Input.GetKeyDown(KeyCode.E))
        {
            _player.SetHidingState();
            _animator.SetBool("isPlayerHiding", _player.IsHiding());
        }
    }
}
