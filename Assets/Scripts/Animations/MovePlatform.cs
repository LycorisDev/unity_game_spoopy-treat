using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private static Vector3 start_pos;
    private static Vector3 target_pos;
    public float speed;
    public float distance;

    void Start()
    {
        start_pos = transform.position;
        target_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
    }

    void Update()
    {
        if (transform.position == start_pos)
            StartCoroutine(LerpCoroutineToEnd());
        if (transform.position == target_pos)
            StartCoroutine(LerpCoroutineToStart());
    }

    private IEnumerator LerpCoroutineToEnd()
    {
        float time = 0f;
 
        while(transform.position != target_pos)
        {
            transform.position = Vector3.Lerp(start_pos, target_pos, (time / Vector3.Distance(start_pos, target_pos)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator LerpCoroutineToStart()
    {
        float time = 0f;
 
        while(transform.position != start_pos)
        {
            transform.position = Vector3.Lerp(target_pos, start_pos, (time / Vector3.Distance(target_pos, start_pos)) * speed);
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
