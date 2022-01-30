using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpin : MonoBehaviour
{
    [SerializeField] PlayerControllerTest playerController;
    [SerializeField] Transform turtleAxis;
    [SerializeField] GameObject turtleOut;
    [SerializeField] GameObject turtleShell;
    [SerializeField] float spinOverTimeStep = 30;
    [SerializeField] float motionMultiplyer = 0.5f;
    private float spinOverTiem = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerControllerTest>();
    }

    // Update is called once per frame
    void Update()
    {
        spinOverTiem += Time.deltaTime;
        if (!playerController.bodyType.hard && turtleShell.activeSelf)
        {
            turtleShell.SetActive(false);
            turtleOut.SetActive(true);
        }
        if (playerController.bodyType.hard && turtleOut.activeSelf)
        {
            turtleOut.SetActive(false);
            turtleShell.SetActive(true);
        }
        turtleAxis.rotation = Quaternion.AngleAxis(playerController.Velocity.x * motionMultiplyer, Vector3.forward)
            * Quaternion.Euler(0, spinOverTiem * spinOverTimeStep, 0);
    }
}
