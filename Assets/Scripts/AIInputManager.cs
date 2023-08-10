using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputManager : InputManager
{
    // Start is called before the first frame update

    public Transform[] waypoints;
    public int currentWaypointIndex = 0;
    private float waypointRadius = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame

    protected override void FixedUpdate()
    {
        Transform target = waypoints[currentWaypointIndex]; // Get the current target waypoint

        // Check if the car has reached the current waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, target.position);
        if (distanceToWaypoint < waypointRadius)
        {
            // Increment index to next waypoint, wrapping around if at end of array
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            target = waypoints[currentWaypointIndex]; // Update target to next waypoint
        }


        throttle = 0.2f;
        Vector3 relativeVector = transform.InverseTransformPoint(target.position);
        steer = (relativeVector.x / relativeVector.magnitude);
        handbrake = false;
    }
}
