using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerV2 : MonoBehaviour
{
    // Start is called before the first frame update
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }

    [SerializeField]private driveType drive;

    public InputManager inputManager;
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float torque = 200f;
    public float maxTurn = 30f;

    public float totalPower;
    public float wheelsRPM;

    public AnimationCurve engineTorque;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimateWheels();

        CalculateEnginePower();
        if (drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = (torque/4) * inputManager.throttle;
            }
        }else if (drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = (torque / 2) * inputManager.throttle;
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length -2; i++)
            {
                wheels[i].motorTorque = (torque / 2) * inputManager.throttle;
            }
        }
        
        for (int i = 0; i < wheels.Length-2; i++)
        {
            wheels[i].steerAngle = maxTurn * inputManager.steer;
            
        }

    }

    void CalculateEnginePower()
    {
        
    }

    void AnimateWheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for(int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
        
    }
}
