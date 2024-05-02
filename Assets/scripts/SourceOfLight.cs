using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        float intensity = Random.Range(minIntensity, maxIntensity);
        light.intensity = intensity;
    }
}