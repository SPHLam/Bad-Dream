using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject[] _enemies;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Remove callback when destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnRandomMonster()
    {

    }



    #region Load Scene

    // This will be called after a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update player reference here
        UIManager.Instance.UpdatePlayerReference();
        PlayerManager.Instance.UpdatePlayerReference();
    }

    public void LoadNextRoom()
    {
        int roomCount = SceneManager.sceneCountInBuildSettings;
        int randomRoomIndex = Random.Range(0, roomCount - 1);
        SceneManager.LoadScene("SampleScene " + randomRoomIndex, LoadSceneMode.Single);
    }
    #endregion
}

