using System.Collections;
using UnityEngine;
public class BreakBrick : MonoBehaviour
{
    BodyType myType;
    [SerializeField] int damageOnImpact = 2;
    private void Start()
    {
        myType = GetComponent<BodyType>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BodyType collidingBodyType;
        if (collision.collider.TryGetComponent<BodyType>(out collidingBodyType))
        {
            CompairBody(collidingBodyType);
        }
    }

    private void CompairBody(BodyType collidingBodyType)
    {
        
        if (myType.hard)
        {
            Debug.Log("MyType = hard");
            if (collidingBodyType.hard)
            {
                Debug.Log("colBody = hard");
                DestroyMe();
                return;
            }
            if (collidingBodyType.soft)
            {
                Debug.Log("colBody = soft");
                if (collidingBodyType.mainCharacter)
                    GiveDamage(damageOnImpact);
            }
        }
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
        return;
    }

    private void GiveDamage(int dmg)
    {
        for (int i = 0; i < dmg; i++)
        {
            HealthController.Instance.SubtractHealth();
        }
    }
}

