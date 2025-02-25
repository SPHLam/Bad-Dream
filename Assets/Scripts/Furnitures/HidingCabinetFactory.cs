using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class HidingCabinetFactory : MonoBehaviour
{
    public static HidingCabinetFactory Instance { get; private set; }

    [SerializeField] List<HidingCabinet> _hidingCabinetPrefabs;
    [SerializeField] int _minCabinets = 5;
    [SerializeField] int _maxCabinets = 7;
    [SerializeField] private int _cabinetWidth = 15;
    [SerializeField] private int _cabinetHeight = 5;

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
        if (_hidingCabinetPrefabs.Count == 0) return null;
        HidingCabinet selectedPrefab = _hidingCabinetPrefabs[Random.Range(0, _hidingCabinetPrefabs.Count)];
        GameObject cabinet = Instantiate(selectedPrefab.gameObject);
        cabinet.GetComponent<HidingCabinet>().Initialize(ReturnToPool);
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
        int numCabinets = Random.Range(_minCabinets, _maxCabinets + 1);

        for (int i = 1; i <= numCabinets; i++)
        {
            float randomX = Random.Range(-50f, 30f); // Random X position between -50 and 30
            float randomY = 1.5f; // Fixed Y position (adjust as needed)

            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            if (!IsPositionOccupided(spawnPosition))
            {
                GameObject cabinet = _pool.Get();
                cabinet.transform.position = spawnPosition;
                _activeCabinets.Add(cabinet);
            }
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

    private bool IsPositionOccupided(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(_cabinetWidth, _cabinetHeight), 0f);
        return colliders.Length > 0;
    }
}
