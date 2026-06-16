using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SubtlePulse : MonoBehaviour
{
    private Light2D myLight;
    
    [Header("Movement Settings")]
    public float moveSpeed = 0.5f;
    public float moveRange = 0.3f;
    
    [Header("Intensity Settings")]
    public float pulseSpeed = 0.4f;
    public float minIntensity = 0.7f;
    public float maxIntensity = 1.1f;

    private Vector3 startPos;

    void Start()
    {
        myLight = GetComponent<Light2D>();
        startPos = transform.localPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        float y = Mathf.Cos(Time.time * moveSpeed * 0.8f) * moveRange;
        transform.localPosition = startPos + new Vector3(x, y, 0);

        float pulse = Mathf.PingPong(Time.time * pulseSpeed, maxIntensity - minIntensity);
        myLight.intensity = minIntensity + pulse;
    }
}