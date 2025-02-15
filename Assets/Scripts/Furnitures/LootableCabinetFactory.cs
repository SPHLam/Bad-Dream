using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement; // Needed for scene events

public class LootableCabinetFactory : MonoBehaviour
{
    public static LootableCabinetFactory Instance { get; private set; }

    [SerializeField] List<LootableCabinet> _lootableCabinetPrefabs;
    [SerializeField] int minCabinets = 3;
    [SerializeField] int maxCabinets = 6;

    private ObjectPool<GameObject> _pool;
    private List<GameObject> _activeCabinets = new List<GameObject>(); // Track spawned cabinets

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        // Initialize object pool
        _pool = new ObjectPool<GameObject>(
            createFunc: InstantiateRandomCabinet,
            actionOnGet: (cabinet) => cabinet.SetActive(true),
            actionOnRelease: (cabinet) => cabinet.SetActive(false),
            actionOnDestroy: (cabinet) => Destroy(cabinet),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Initial spawn (if needed)
        SpawnCabinets();
    }

    private GameObject InstantiateRandomCabinet()
    {
        if (_lootableCabinetPrefabs.Count == 0) return null;
        LootableCabinet selectedPrefab = _lootableCabinetPrefabs[Random.Range(0, _lootableCabinetPrefabs.Count)];
        GameObject cabinet = Instantiate(selectedPrefab.gameObject);
        cabinet.GetComponent<LootableCabinet>().Initialize(ReturnToPool);
        return cabinet;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetCabinets(); // Remove old cabinets
        ClearPool();     // Completely reset the pool
        SpawnCabinets(); // Spawn new cabinets
    }

    public void SpawnCabinets()
    {
        int numCabinets = Random.Range(minCabinets, maxCabinets + 1);

        for (int i = 0; i < numCabinets; i++)
        {
            float randomX = Random.Range(-50f, 30f); // Random X position between -50 and 30
            float randomY = 1.5f; // Fixed Y position (adjust as needed)

            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            GameObject cabinet = _pool.Get();
            cabinet.transform.position = spawnPosition;
            cabinet.GetComponent<LootableCabinet>().AssignRandomItem(); // Assigns medkit, battery, or nothing
            _activeCabinets.Add(cabinet);
        }
    }

    private void ResetCabinets()
    {
        foreach (var cabinet in _activeCabinets)
        {
            if (cabinet != null)
            {
                _pool.Release(cabinet); // Return all cabinets to pool
            }
        }
        _activeCabinets.Clear(); // Clear the tracking list
    }

    private void ClearPool()
    {
        _pool.Clear(); // Clears all pooled objects to prevent referencing destroyed ones
    }

    private void ReturnToPool(GameObject cabinet)
    {
        if (cabinet == null) return; // Prevent adding destroyed objects to the pool
        _pool.Release(cabinet);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe event to prevent memory leaks
    }
}
