using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public Light2D light2D;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float minRadius = 0.7f;
    public float maxRadius = 0.8f;
    public float flickerSpeed = 0.11f;

    private void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            light2D.pointLightOuterRadius = Random.Range(minRadius, maxRadius);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}


