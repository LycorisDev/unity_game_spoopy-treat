using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlickeringAnimation : MonoBehaviour
{
    private Light lightComponent;
    private static float minRange, maxRange;
    private static float minIntensity, maxIntensity;

    void Awake()
    {
        lightComponent = GetComponent<Light>();
    }

    void Start()
    {
        minRange = 1.5f;
        maxRange = 2f;
        minIntensity = 1.8f;
        maxIntensity = 2f;
    }

    void Update()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        lightComponent.range = UnityEngine.Random.Range(minRange, maxRange);
        lightComponent.intensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
        yield return new WaitForSecondsRealtime(5f);
    }
}
