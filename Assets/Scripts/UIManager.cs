using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Image _batteryBorder;
    [SerializeField] private Image _batteryBar;
    [SerializeField] private Image _transitionImage;
    private Player _player;
    private float _flashlightBatteryAmount;
    private float _flashingSpeed = 5f;

    [SerializeField] private List<Button> inventorySlots = new List<Button>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);

        SetupInventoryButtons();
    }
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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
        // Re-setup buttons after scene load
        SetupInventoryButtons();

        // Verify EventSystem exists
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogWarning("No EventSystem found! Creating one...");
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _flashlightBatteryAmount = PlayerManager.Instance.flashLightBatteryAmmount;
        _batteryBar.fillAmount = _flashlightBatteryAmount / 100f;

        // Update the color based on battery percentage
        if (_flashlightBatteryAmount > 50f)
        {
            _batteryBorder.color = Color.green;
            _batteryBar.color = Color.green;
        }
        else if (_flashlightBatteryAmount > 20f)
        {
            _batteryBorder.color = Color.yellow;
            _batteryBar.color = Color.yellow;
        }
        else if (_flashlightBatteryAmount > 0f)
        {
            _batteryBorder.color = Color.red;
            _batteryBar.color = Color.red;
        }
        else
        {
            Color currentColor = _batteryBorder.color; // Keep red when flashing
            currentColor.a = Mathf.Sin(Time.time * _flashingSpeed) > 0 ? 1f : 0f;
            _batteryBorder.color = currentColor;
        }
    }

    public void UpdatePlayerReference()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void SetupInventoryButtons()
    {
        Transform canvas = transform.Find("Canvas");

        if (canvas != null)
        {
            inventorySlots.Clear();
            Button[] buttons = canvas.GetComponentsInChildren<Button>();

            inventorySlots.AddRange(buttons);

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                int slotIndex = i;
                Button btn = inventorySlots[i];
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => PlayerManager.Instance.OnInventorySlotClicked(slotIndex));
            }
        }
    }

    // Method to be called from PlayerManager
    public void UpdateInventoryDisplay(Dictionary<string, int> itemCounts, Dictionary<string, Sprite> itemSprites)
    {
        int index = 0;
        foreach (var entry in itemCounts)
        {
            if (index >= inventorySlots.Count) break;

            Transform slotTransform = inventorySlots[index].transform;
            Image buttonImage = slotTransform.Find("ItemIcon")?.GetComponent<Image>();
            TMP_Text countText = slotTransform.Find("ItemCount")?.GetComponent<TMP_Text>();

            if (buttonImage != null && itemSprites.ContainsKey(entry.Key))
            {
                buttonImage.sprite = itemSprites[entry.Key];
                buttonImage.enabled = true;
                var color = buttonImage.color;
                color.a = 1f;
                buttonImage.color = color;
            }

            if (countText != null)
            {
                countText.text = entry.Value.ToString();
            }

            index++;
        }

        // Hide remaining slots
        for (; index < inventorySlots.Count; index++)
        {
            Transform slotTransform = inventorySlots[index].transform;
            Image buttonImage = slotTransform.Find("ItemIcon")?.GetComponent<Image>();
            TMP_Text countText = slotTransform.Find("ItemCount")?.GetComponent<TMP_Text>();

            if (buttonImage != null)
            {
                buttonImage.sprite = null;
                buttonImage.enabled = false;
                var color = buttonImage.color;
                color.a = 0f;
                buttonImage.color = color;
            }

            if (countText != null)
            {
                countText.text = "";
            }
        }
    }
}
