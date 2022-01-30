using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] Transform fruit;
    private float angle = 0;
    public void Eat(PlayerControllerTest pl = null)
    {
        pl.AddScore(score);
        Destroy(gameObject);
    }

    public void Update()
    {
        angle += 100 * Time.deltaTime;
        fruit.rotation = Quaternion.AngleAxis( angle, Vector3.one);
    }
}
