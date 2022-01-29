using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomShard : MonoBehaviour
{
    [SerializeField] private float speed = 16;

    public IEnumerator BOOM(Vector3 dir)
    {
        float rand = Random.Range(-360, 360);
        float time = 4;
        float scaleLoss = 1 / time;
        while (time > 0)
        {
            transform.position += dir * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, rand * Time.deltaTime, 0);
            transform.localScale -= Vector3.one * scaleLoss * Time.deltaTime;
            yield return null;
            time -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}