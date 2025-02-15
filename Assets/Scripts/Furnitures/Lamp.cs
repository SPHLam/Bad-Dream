using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    // COMPONENTS
    private Light2D _light;
    private SpriteRenderer _spriteRenderer;

    // MATERIAL
    [SerializeField] private Material _spriteLitDefaultMaterial;

    // FLICKERING
    [SerializeField] float _flickerSpeed = 20f; // How fast the light flickers
    [SerializeField] float _intensityMin = 0f; // Minimum intensity
    [SerializeField] float _intensityMax = 0.75f; // Maximum intensity
    bool _isFlickering = false;

    private void Awake()
    {
        _light = GetComponentInChildren<Light2D>();

        if (_light == null)
        {
            Debug.LogError("No Light2D component found on this GameObject.");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError("No Sprite Renderer component found on this GameObject.");
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (_isFlickering)
            Flickering();
    }

    #region Flickering
    public void Flickering()
    {
        if (_light != null)
        {
            // Add randomness by using PerlinNoise with high-speed input
            float randomValue = Mathf.PerlinNoise(Time.time * _flickerSpeed, Random.Range(0f, 100f));

            // Scale the random value to the desired intensity range
            _light.intensity = Mathf.Lerp(_intensityMin, _intensityMax, randomValue);
        }
    }

    public void SetFlickering()
    {
        _isFlickering = true;
    }

    #endregion

    #region Broken

    public void BreakLamp()
    {
        _isFlickering = false;

        if (_light != null)
        {
            Destroy(_light);
            _light = null;
        }

        if (_spriteRenderer != null)
            _spriteRenderer.material = _spriteLitDefaultMaterial;
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Phantom"))
        {
            Debug.Log("Phantom");
            BreakLamp();
        }
    }
}
