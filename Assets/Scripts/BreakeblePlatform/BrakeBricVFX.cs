using UnityEngine;

public class BrakeBricVFX : MonoBehaviour
{
    [SerializeField] private BreakBrick parent;
    [SerializeField] private GameObject boxL;
    [SerializeField] private GameObject boxR;

    private Vector3 size;
    public float holeSize = 1f;
    public Vector3 holePos = Vector3.zero;
    
    public void Init(BreakBrick parent)
    {
        this.parent = parent;
        ResetPlatform();
    }

    public void ResetPlatform()
    {
        size = parent.collider.size;

        float segment = size.x / 2;

        boxL.transform.localScale = new Vector3(segment, size.y, 1);
        boxL.transform.position = transform.position + new Vector3(-segment * 0.5f, 0, 0);

        boxR.transform.localScale = new Vector3(segment, size.y, 1);
        boxR.transform.position = transform.position + new Vector3(segment * 0.5f, 0, 0);
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


        float lPos = lCorner - lSize;
        float rPos = rCorner - rSize;
        Debug.DrawRay(Vector3.right * lPos, Vector3.up, Color.yellow);
        Debug.DrawRay(Vector3.right * rPos, Vector3.up, Color.yellow);

        boxL.transform.position = new Vector3(lPos, transform.position.y, transform.position.z);
        boxL.transform.localScale = new Vector3(lSize * 2, size.y, 1);

        boxR.transform.position = new Vector3(rPos, transform.position.y, transform.position.z);
        boxR.transform.localScale = new Vector3(rSize * 2, size.y, 1);
    }
}
