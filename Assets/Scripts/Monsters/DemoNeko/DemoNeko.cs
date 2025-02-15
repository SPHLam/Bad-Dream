using UnityEngine;

[RequireComponent(typeof(DemoNekoMovement))]
public class DemoNeko : MonoBehaviour
{
    public Animator animator;
    private SpriteRenderer _spriteRenderer;
    public DemoNekoMovement demoNekoMovement;
    private Player _player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        demoNekoMovement = GetComponent<DemoNekoMovement>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleFlipAnimation();
    }

    private void HandleFlipAnimation()
    {
        if (_player.transform.position.x < transform.position.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }
}
