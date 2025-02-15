using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _floatSpeed = 3f;
    [SerializeField] private float _driftSpeed = 3f;
    [SerializeField] private float _movementRange = 0.5f;

    private float _randomFloatSpeed;
    private float _randomDriftSpeed;
    private float _randomMovementRange;

    private Vector3 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        InitializeRandomValues();
    }

    // Update is called once per frame
    void Update()
    {
        HandleFloating();
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void InitializeRandomValues()
    {
        // Randomize float speed, drift speed, and movement range
        _randomFloatSpeed = Random.Range(_floatSpeed * 0.5f, _floatSpeed * 1.5f);
        _randomDriftSpeed = Random.Range(_driftSpeed * 0.5f, _driftSpeed * 1.5f);
        _randomMovementRange = Random.Range(_movementRange * 0.5f, _movementRange * 1.5f);
    }

    private void HandleFloating()
    {
        float x = Mathf.PerlinNoise(Time.time * _randomDriftSpeed, 0) * 2 - 1;
        float y = Mathf.PerlinNoise(0, Time.time * _randomFloatSpeed) * 2 - 1;

        transform.position = _startPosition + new Vector3(x, y, 0) * _randomMovementRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            Pop();
        }
    }

    private void Pop()
    {
        gameObject.SetActive(false);
        BalloonObjectPool.Instance.Release(this);
    }
}
