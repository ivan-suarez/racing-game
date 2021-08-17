using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{

    public Transform path; 
    private List<Transform> nodes;
    private int currentNode = 0;

    public float maxSteerAngle = 45f;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    public bool isBraking = false;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public float frontSensorPosition = 0.5f;
    public float frontSideSensorPosition = 0.2f;
    public float frontSensorAngle = 30;
   
    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
        //Debug.Log(currentNode);
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos.z = frontSensorPosition;

        //front center sensor 
        if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
        //front right sensor
        sensorStartPos.x += frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
        //front right angle sensor
        
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
        //front left sensor
        sensorStartPos.x += 2 * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
       // Gizmos.DrawLine(sensorStartPos, hit.point);

        //front left angle sensor

        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
    }
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude)*maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed< maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
        
    }

    private void CheckWaypointDistance()
    {
        //Debug.Log((Vector3.Distance(transform.position, nodes[currentNode].position)));
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f) 
        {
            
            if (currentNode==(nodes.Count - 1))
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
    private void Braking()
    {
        if (isBraking)
        {
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }
}
