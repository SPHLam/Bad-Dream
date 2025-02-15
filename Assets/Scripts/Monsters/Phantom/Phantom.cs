using UnityEngine;

public class Phantom : Enemy
{
    [SerializeField] private float _speed = 15f;
    private bool _playerTriggered;

    // EFFECTS
    private float _timeBetweenSpawns;
    [SerializeField] private float _startTimeBetweenSpawns;
    [SerializeField] private GameObject _echoEffect;
    // Start is called before the first frame update
    void Start()
    {
        FlickeringLight();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTriggered)
        {
            Charge();
            EchoSpawing();
        }
    }

    public void Charge()
    {
        transform.position += Vector3.right * _speed * Time.deltaTime;
    }

    public void SetCharging()
    {
        _playerTriggered = true;
    }

    public void FlickeringLight()
    {
        Lamp[] allLamps = FindObjectsOfType<Lamp>();
        foreach (Lamp lamp in allLamps)
        {
            if (lamp != null)
                lamp.SetFlickering();
        }
    }

    public void EchoSpawing()
    {
        if (_timeBetweenSpawns < 0)
        {
            // Spawn echo game object
            GameObject echoInstance = Instantiate(_echoEffect, transform.position, Quaternion.identity);
            Destroy(echoInstance, 1f);
            _timeBetweenSpawns = _startTimeBetweenSpawns;
        }
        else
        {
            _timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
