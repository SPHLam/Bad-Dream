using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private Player _player;

    /* FLASHLIGHT INFO */
    public float flashLightBatteryAmmount = 100f;
    [SerializeField] private float _batteryDrainRate = 0.01f;
    [SerializeField] private int _healAmount = 30;

    /* HEALTH INFO */
    private int _health = 100;

    /* INVENTORY INFO */
    [SerializeField] List<Button> inventorySlots = new List<Button>();
    private Dictionary<string, int> _itemCounts = new Dictionary<string, int>();
    private Dictionary<string, ILootableItem> _itemPrototypes = new Dictionary<string, ILootableItem>();
    private Dictionary<string, Sprite> _itemSprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerReference();
    }

    // Update is called once per frame
    void Update()
    {
        HandleFlashlightBattery();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update player reference (you already have this)
        UpdatePlayerReference();

        // Update the UI with current inventory state
        UpdateInventoryUI();
    }

    public void UpdatePlayerReference()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    #region Flashlight
    private void HandleFlashlightBattery()
    {
        if (_player.isFlashlightOn)
        {
            flashLightBatteryAmmount -= _batteryDrainRate;
        }

        if (flashLightBatteryAmmount <= 0)
            _player.SetBatteryDied();
        else
            _player.BatteryCharged();
    }

    public void RechargeFlashlight()
    {
        flashLightBatteryAmmount = 100f;
    }
    #endregion

    #region Health

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _player.Die();
        }
    }

    public void Heal()
    {
        _health += _healAmount;
        if (_health > 100)
        {
            _health = 100;
        }
    }

    #endregion

    #region Inventory

    //private void SetupInventoryButtons()
    //{
    //    // Add click listeners to all inventory buttons
    //    for (int i = 0; i < inventorySlots.Count; i++)
    //    {
    //        int slotIndex = i; // Capture the index for the lambda
    //        inventorySlots[i].onClick.AddListener(() => OnInventorySlotClicked(slotIndex));
    //    }
    //}

    private void UpdateInventoryUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateInventoryDisplay(_itemCounts, _itemSprites);
        }
    }

    public void OnInventorySlotClicked(int slotIndex)
    {
        Debug.Log($"Slot {slotIndex} clicked"); // Add this line

        int currentIndex = 0;
        foreach (var entry in _itemCounts)
        {
            if (currentIndex == slotIndex)
            {
                Debug.Log($"Found item: {entry.Key}"); // Add this line
                if (_itemPrototypes.ContainsKey(entry.Key))
                {
                    Debug.Log("Using item"); // Add this line
                    _itemPrototypes[entry.Key].Use();
                    RemoveItem(_itemPrototypes[entry.Key]);
                }
                break;
            }
            currentIndex++;
        }
    }

    public void AddItem(ILootableItem item)
    {
        if (!_itemPrototypes.ContainsKey(item.GetName()))
        {
            _itemPrototypes[item.GetName()] = item;
        }

        if (_itemCounts.ContainsKey(item.GetName()))
            _itemCounts[item.GetName()]++;
        else
        {
            _itemSprites[item.GetName()] = item.GetSprite();
            _itemCounts[item.GetName()] = 1;
        }

        UpdateInventoryUI();
    }

    public void RemoveItem(ILootableItem item)
    {
        if (_itemCounts.ContainsKey(item.GetName()))
        {
            _itemCounts[item.GetName()]--;
            if (_itemCounts[item.GetName()] <= 0)
            {
                _itemCounts.Remove(item.GetName());
            }
        }

        UpdateInventoryUI();
    }
    #endregion
}
