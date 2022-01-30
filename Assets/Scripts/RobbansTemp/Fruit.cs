using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] Transform fruit;
    private float angle = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent( out PlayerControllerTest pl))
        {
            GameManager.Instance.AddScore(score);
        }
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
    /*
    public void Eat(PlayerControllerTest pl = null)
    {
        pl.AddScore(score);
        Destroy(gameObject);
    }
    */

    public void Update()
    {
        angle += 100 * Time.deltaTime;
        fruit.rotation = Quaternion.AngleAxis( angle, Vector3.one);
    }
}
