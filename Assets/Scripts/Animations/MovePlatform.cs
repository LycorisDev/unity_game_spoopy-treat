using System.Collections;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _distance = 15f;
    private Vector3 _startPos;
    private Vector3 _targetPos;

    private void Start()
    {
        _startPos = transform.position;
        _targetPos = _startPos + new Vector3(0f, 0f, _distance);
    }

    private void Update()
    {
        if (transform.position == _startPos)
            StartCoroutine(LerpCoroutineToEnd());
        if (transform.position == _targetPos)
            StartCoroutine(LerpCoroutineToStart());
    }

    private void OnCollisionEnter(Collision other)
    {
        other.transform.SetParent(transform, true);
    }

    private void OnCollisionExit(Collision other)
    {
        other.transform.parent = null;
    }

    private IEnumerator LerpCoroutineToEnd()
    {
        float time = 0f;
 
        while (transform.position != _targetPos)
        {
            transform.position = Vector3.Lerp(_startPos, _targetPos, (time / Vector3.Distance(_startPos, _targetPos)) * _speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator LerpCoroutineToStart()
    {
        float time = 0f;
 
        while (transform.position != _startPos)
        {
            transform.position = Vector3.Lerp(_targetPos, _startPos, (time / Vector3.Distance(_targetPos, _startPos)) * _speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
