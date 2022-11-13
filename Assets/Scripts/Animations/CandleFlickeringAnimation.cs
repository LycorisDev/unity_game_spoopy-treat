using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlickeringAnimation : MonoBehaviour
{
    private static float min_range, max_range;
    private static float min_intensity, max_intensity;
    private Light light_component;

    void Start()
    {
        min_range = 1.5f;
        max_range = 2f;
        min_intensity = 1.8f;
        max_intensity = 2f;
        light_component = GetComponent<Light>();
    }

    void Update()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        light_component.range = UnityEngine.Random.Range(min_range, max_range);
        light_component.intensity = UnityEngine.Random.Range(min_intensity, max_intensity);
        yield return new WaitForSecondsRealtime(5f);
    }
}
