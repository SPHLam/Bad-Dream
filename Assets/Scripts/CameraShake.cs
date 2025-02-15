using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _myCamera;
    private CinemachineBasicMultiChannelPerlin m_noise;

    public float maxShakeDistance = 80f; // Maximum distance where the shake is felt
    public float maxAmplitude = 2.5f;     // Maximum amplitude for the shake
    public float maxFrequency = 2.5f;     // Maximum frequency for the shake
    private void Awake()
    {
        _myCamera = GetComponent<CinemachineVirtualCamera>();
        if (_myCamera == null)
            Debug.Log("Camera is null");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_myCamera != null)
            m_noise = _myCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject phantom = GameObject.FindGameObjectWithTag("Phantom");

        if (phantom != null)
        {
            float distance = Vector2.Distance(phantom.transform.position, Camera.main.transform.position);

            // Calculate shake intensity based on distance
            float intensity = Mathf.Clamp01(1 - (distance / maxShakeDistance)); // Normalize intensity (0 to 1)

            // Smoothly adjust amplitude and frequency
            m_noise.m_AmplitudeGain = intensity * maxAmplitude; // Scale amplitude
            m_noise.m_FrequencyGain = intensity * maxFrequency; // Scale frequency
        }
    }
}
