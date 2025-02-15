using UnityEngine;
using UnityEngine.Pool;

public class BalloonObjectPool : MonoBehaviour
{
    public static BalloonObjectPool Instance { get; private set; }
    [SerializeField] private GameObject _balloonPrefab;

    private ObjectPool<Balloon> _pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _pool = new ObjectPool<Balloon>(createFunc: () => Instantiate(_balloonPrefab).GetComponent<Balloon>(),
            actionOnGet: balloon => balloon.gameObject.SetActive(true),
            actionOnRelease: balloon => balloon.gameObject.SetActive(false),
            actionOnDestroy: balloon => Destroy(balloon.gameObject),
            collectionCheck: false,
            defaultCapacity: 15,
            maxSize: 30);
    }

    public Balloon Get()
    {
        return _pool.Get();
    }

    public void Release(Balloon balloon)
    {
        _pool.Release(balloon);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
