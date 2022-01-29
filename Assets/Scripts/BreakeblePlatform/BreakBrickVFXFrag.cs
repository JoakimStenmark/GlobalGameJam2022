using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrickVFXFrag : MonoBehaviour
{
    [SerializeField] private BreakBrick parent;
    private Vector3 size;
    public float holeSize = 1f;
    public Vector3 holePos = Vector3.zero;
    public GameObject fragGroupePreFab;
    List<BoomShard> frags;

    public void Init(BreakBrick parent)
    {
        this.parent = parent;
        ResetPlatform();




        frags = new List<BoomShard>();
        size = parent.collider.size;
        float segment = size.x / 2;
        float hSegemnt = size.y / 2;

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 pos = transform.position - Vector3.right * (segment - x - 0.5f) - Vector3.up * (hSegemnt - y - 0.5f);
                GameObject frag = Instantiate(fragGroupePreFab, pos, Quaternion.Euler(0, 180, 0), transform);
                foreach (Transform t in frag.transform)
                {
                    if (t.TryGetComponent<BoomShard>(out BoomShard boom))
                    {
                        frags.Add(boom);
                    }
                }
            }

        }
    }
    public void ResetPlatform()
    {
        Debug.Log("RESET : not implamented");
    }

    public void BreakAtWorlPoint(Vector3 point)
    {
        float lHoleSide = point.x - holeSize;
        float rHoleSide = point.x + holeSize;
        Debug.DrawRay(Vector3.right * lHoleSide, Vector3.up, Color.red);
        Debug.DrawRay(Vector3.right * rHoleSide, Vector3.up, Color.red);

        MakeHole(lHoleSide, rHoleSide, point);
    }

    public void MakeHole(float leftX, float rightX, Vector3 point)
    {
        Debug.DrawRay(new Vector3(leftX, transform.position.y, transform.position.z), Vector3.up, Color.red, 5);
        Debug.DrawRay(new Vector3(rightX, transform.position.y, transform.position.z), Vector3.up, Color.red, 5);
        foreach (BoomShard boom in frags)
        {
            if (boom.transform.position.x > leftX && boom.transform.position.x < rightX)
            {
                Vector3 dir = boom.transform.position - (point + Vector3.up);
                StartCoroutine(boom.BOOM(dir));
            }
        }
    }
}