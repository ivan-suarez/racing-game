using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
public class CarController : MonoBehaviour
{
    public InputManager inputManager;
    public List<WheelCollider> steeringWheels;
    public List<WheelCollider> throttleWheels;

    public float strengthCoefficient = 250f;
    public float maxTurn = 20f;

    public AnimationCurve torqueCurve;
    public float[] gearRatios = { 3.794f, 2.324f, 1.624f, 1.271f, 1.000f, 0.794f };
    public int currentGear = 0;
    public float shiftUpRPM = 6000f;  // RPM value to shift up
    public float shiftDownRPM = 3000f;  // RPM value to shift down
    public float finalDrive = 3.538f;  // Final drive ratio
    public float currentRPM;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();

        // setup an example torque curve
        torqueCurve = new AnimationCurve();
        torqueCurve.AddKey(0, 100);     // idle torque
        torqueCurve.AddKey(3000, 200);
        torqueCurve.AddKey(5000, 300);  // peak torque
        torqueCurve.AddKey(6500, 100);  // redline torque
    }

    // Update is called once per frame
    void FixedUpdate()
    {

         // Calculate the current engine RPM
        WheelCollider firstWheel = throttleWheels[0];
        currentRPM = firstWheel.rpm * gearRatios[currentGear] * finalDrive;

        // Get the current engine torque
        float currentTorque = torqueCurve.Evaluate(currentRPM);

        // Shift gears
        // shift up
        if (currentRPM > shiftUpRPM && currentGear < gearRatios.Length - 1)
        {
            currentGear++;
        }
        // shift down
        else if (currentRPM < shiftDownRPM && currentGear > 0)
        {
            currentGear--;
        }

        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = currentTorque * strengthCoefficient * Time.deltaTime * inputManager.throttle;
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * inputManager.steer;
        }

        //Debug the car's speed in KM/H
        Debug.Log("Speed: " + GetComponent<Rigidbody>().velocity.magnitude * 3.6f + " KM/H");
        GUI.Label(new Rect(20, 20, 200, 20), "Gear: " + currentGear);
    }
}