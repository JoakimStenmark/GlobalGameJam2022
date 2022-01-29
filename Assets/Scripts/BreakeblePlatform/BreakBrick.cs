using System.Collections;
using UnityEngine;
public class BreakBrick : MonoBehaviour
{
    BodyType myType;
    [SerializeField] int damageOnImpact = 2;
    [SerializeField] public BoxCollider collider;

    [SerializeField] BrakeBricVFX vfx;
    private void Start()
    {
        myType = GetComponent<BodyType>();

        if (collider == null)
            collider = GetComponent<BoxCollider>();

        if (vfx == null)
            vfx = GetComponent<BrakeBricVFX>();

        vfx.Init(this);
    }

    public void ResetPlatform()
    {
        collider.enabled = true;
        vfx.ResetPlatform();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.collider.TryGetComponent<BodyType>(out BodyType collidingBodyType))
        {
            Debug.Log("Found bodytype");
            CompairBody(collidingBodyType, collision.GetContact(0).point);
        }
    }

    private void CompairBody(BodyType compairedType, Vector3 point)
    {

        if (myType.hard)
        {
            Debug.Log("MyType = hard");
            if (compairedType.hard)
            {
                Debug.Log("colBody = hard");
                 
                DestroyMe(point);
                return;
            }
            if (!compairedType.hard)
            {
                Debug.Log("colBody = soft");
                if (compairedType.mainCharacter)
                    GiveDamage(damageOnImpact);
            }
        }
    }

    private void DestroyMe(Vector3 point)
    {
        vfx.BreakAtWorlPoint(point);
        collider.enabled = false;
            // Destroy(this.gameObject);
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
