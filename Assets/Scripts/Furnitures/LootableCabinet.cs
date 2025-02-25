using UnityEngine;

public class LootableCabinet : MonoBehaviour
{
    [SerializeField] private Sprite _medkitSprite;
    [SerializeField] private Sprite _batterySprite;
    private ILootableItem itemEffect;
    private System.Action<GameObject> _returnToPool;
    private bool _isPlayerColliding;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AssignRandomItem()
    {
        float chance = Random.value;

        if (chance < 0.33f)
            itemEffect = new Medkit(_medkitSprite);
        else if (chance < 0.66f)
            itemEffect = new Battery(_batterySprite);
        else
            itemEffect = null;
    }

    public void Initialize(System.Action<GameObject> poolReturn)
    {
        _returnToPool = poolReturn;
        AssignRandomItem();
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

    private void Update()
    {
        HandleOpeningCabinet();
    }

    public void HandleOpeningCabinet()
    {
        if (_isPlayerColliding && Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("isOpen", true);
            if (itemEffect != null)
            {
                itemEffect.AddToInventory();
                //Debug.Log("Found: " + itemEffect.GetName());
                itemEffect = null;
            }
            else
            {
                Debug.Log("Found nothing");
            }
        }

    }
    private void OnDisable()
    {
        _returnToPool?.Invoke(gameObject);
    }
}
