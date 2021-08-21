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

    public AnimationCurve enginePower;

    public float engineRPM;
    public float smoothTime = 0.01f;
    public float[] gears;
    public int gearNum = 0;

    public float maxRPM, minRPM;
    public float KPH;


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
                //wheels[i].motorTorque = (totalPower / 4);
            }
        }else if (drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                //wheels[i].motorTorque = (torque / 2) * inputManager.throttle;
                wheels[i].motorTorque = (totalPower / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length -2; i++)
            {
                wheels[i].motorTorque = (torque / 2) * inputManager.throttle;
                //wheels[i].motorTorque = (totalPower / 2);
            }
        }
        
        for (int i = 0; i < wheels.Length-2; i++)
        {
            wheels[i].steerAngle = maxTurn * inputManager.steer;
            
        }
        Shifter();
       // KPH = wheels[0].rpm;
        KPH = (float)((wheels[0].rpm * (2*wheels[0].radius * 3.14))*0.06);
    }

    void CalculateEnginePower()
    {
        wheelRPM();
        totalPower = enginePower.Evaluate(engineRPM) * (gears[gearNum]) * inputManager.throttle;
        //totalPower = 600 * inputManager.throttle;

        float velocity = 0.0f;

     /*   if (engineRPM >= maxRPM || flag)
        {
            engineRPM = Mathf.SmoothDamp(engineRPM, maxRPM - 500, ref velocity, 0.05f);

            flag = (engineRPM >= maxRPM - 450) ? true : false;
           
        }
        else
        {
            engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);
            
        }
        if (engineRPM >= maxRPM + 1000) engineRPM = maxRPM + 1000; // clamp at max
     */
        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f* (gears[gearNum])), ref velocity, smoothTime);
    }

    void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
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

    void Shifter()
    {

        if(engineRPM > maxRPM && gearNum < gears.Length -1)
        {
            gearNum++;
           
        }
        if (engineRPM < minRPM && gearNum > 0)
        {
            gearNum--;

        }
       /* if (Input.GetKeyDown(KeyCode.E))
        {
            gearNum++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gearNum--;
        }*/
    }
}
