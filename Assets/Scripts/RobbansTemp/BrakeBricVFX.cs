using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BrakeBricVFX : MonoBehaviour
{
    [SerializeField] private BreakBrick parent;
    [SerializeField] private GameObject boxL;
    [SerializeField] private GameObject boxR;

    private Vector3 size;
    public float holeSize = 1f;
    public Vector3 holePos = Vector3.zero;

    public GameObject fragmentGroup;

    List<BoomShard> booms;

    public void Init(BreakBrick parent)
    {
        this.parent = parent;
        ResetPlatform();
    }

    public void ResetPlatform()
    {

        booms = new List<BoomShard>();

        size = parent.collider.size;

        float segment = size.x / 2;

        for (int i = 0; i < size.x; i++)
        {
            Vector3 pos = transform.position - Vector3.right * (segment - i - 0.5f);
            GameObject frag = Instantiate(fragmentGroup, pos, Quaternion.Euler(0, 180, 0), transform);
            foreach (Transform t in frag.transform)
            {
                if (t.TryGetComponent<BoomShard>(out BoomShard boom))
                {
                    booms.Add(boom);
                }
            }
        }
        Debug.Log("ListLegnth" + booms.Count);
    }



    public void BreakAtWorlPoint(Vector3 point)
    {
        float lHoleSide = point.x - holeSize;
        float rHoleSide = point.x + holeSize;
        Debug.DrawRay(Vector3.right * lHoleSide, Vector3.up, Color.red);
        Debug.DrawRay(Vector3.right * rHoleSide, Vector3.up, Color.red);

        float lCorner = transform.position.x - size.x * 0.5f;
        float rCorner = transform.position.x + size.x * 0.5f;
        Debug.DrawRay(Vector3.right * lCorner, Vector3.up, Color.blue);
        Debug.DrawRay(Vector3.right * rCorner, Vector3.up, Color.blue);


        float lSize;
        if (lHoleSide < lCorner)
        {
            lSize = 0;
        }
        else if (lHoleSide > rCorner)
        {
            lSize = size.x * -0.5f;
        }
        else
        {
            lSize = (lCorner - lHoleSide) * 0.5f;
        }


        float rSize;
        if (rHoleSide > rCorner)
        {
            rSize = 0;
        }
        else if (rHoleSide < lCorner)
        {
            rSize = size.x * 0.5f;
        }
        else
        {
            rSize = (rCorner - rHoleSide) * 0.5f;
        }

        MakeHole(lHoleSide, rHoleSide, point);
        float lPos = lCorner - lSize;
        float rPos = rCorner - rSize;
        Debug.DrawRay(Vector3.right * lPos, Vector3.up, Color.yellow);
        Debug.DrawRay(Vector3.right * rPos, Vector3.up, Color.yellow);
    }

    public void MakeHole(float leftX, float rightX, Vector3 point)
    {
        Debug.DrawRay(new Vector3(leftX, transform.position.y, transform.position.z), Vector3.up, Color.red, 5);
        Debug.DrawRay(new Vector3(rightX, transform.position.y, transform.position.z), Vector3.up, Color.red, 5);
        foreach (BoomShard boom in booms)
        {
            if (boom.transform.position.x > leftX && boom.transform.position.x < rightX)
            {
                Vector3 dir = boom.transform.position - (point + Vector3.up);
                StartCoroutine(boom.BOOM(dir));
            }
        }
    }
}