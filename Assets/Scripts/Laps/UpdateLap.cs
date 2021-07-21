using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLap : MonoBehaviour
{

    public Player player;
    public Text lapCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lapCount.text = "Lap: " + player.lapNumber;
    }
}
