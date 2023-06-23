using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CandleFlickeringAnimation : MonoBehaviour
{
    private static float _minRange = 1.5f, _maxRange = 2f;
    private static float _minIntensity = 1.8f, _maxIntensity = 2f;
    private Light _lightComponent;

    private void Awake()
    {
        _lightComponent = GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        _lightComponent.range = Random.Range(_minRange, _maxRange);
        _lightComponent.intensity = Random.Range(_minIntensity, _maxIntensity);
        yield return new WaitForSecondsRealtime(5f);
    }
}
