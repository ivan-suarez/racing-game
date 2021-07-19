using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
   // public float speed;
    private Rigidbody rigidbody;

    public int lapNumber;
    public int checkpointIndex;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lapNumber = 1;
        checkpointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}