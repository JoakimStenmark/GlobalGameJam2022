using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessureTejp : MonoBehaviour
{
    [SerializeField] private GameObject preFab;

    List<GameObject> messureTejp;

    // Start is called before the first frame update
    void Start()
    {
        messureTejp = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject instance = Instantiate(preFab, transform.position + Vector3.up * i * 2 + Vector3.forward * 40, Quaternion.identity);
            messureTejp.Add(instance);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float yMax = Camera.main.transform.position.y + 10;
        if (messureTejp[messureTejp.Count - 1].transform.position.y > yMax)
        {
            GameObject moveObj = messureTejp[messureTejp.Count - 1];
            moveObj.transform.position = messureTejp[0].transform.position - Vector3.up*2;
            messureTejp.RemoveAt(messureTejp.Count - 1);
            messureTejp.Insert(0, moveObj);
        }

    }
}
