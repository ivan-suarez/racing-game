using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestLap : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textBox;
    public float bestLap = 100000f;

    public List<float> lapTimes = new List<float>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        calcBestLap();
        textBox.text = "Best lap: " + Math.Round(bestLap, 3);
    }

    void calcBestLap()
    {
        
        foreach (float lap in lapTimes)
        {
            if (lap < bestLap)
                bestLap = lap;
        }
        
    }
}
