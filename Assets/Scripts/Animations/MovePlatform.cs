using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private static Vector3 startPos;
    private static Vector3 targetPos;
    public float speed;
    public float distance;

    void Start()
    {
        startPos = transform.position;
        targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
    }

    void Update()
    {
        if (transform.position == startPos)
            StartCoroutine(LerpCoroutineToEnd());
        if (transform.position == targetPos)
            StartCoroutine(LerpCoroutineToStart());
    }

    private IEnumerator LerpCoroutineToEnd()
    {
        float time = 0f;
 
        while(transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, (time / Vector3.Distance(startPos, targetPos)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator LerpCoroutineToStart()
    {
        float time = 0f;
 
        while(transform.position != startPos)
        {
            transform.position = Vector3.Lerp(targetPos, startPos, (time / Vector3.Distance(targetPos, startPos)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        other.gameObject.transform.SetParent(transform, true);
    }

    void OnCollisionExit(Collision other)
    {
        other.gameObject.transform.parent = null;
    }
}
