using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapHandle : MonoBehaviour
{

    public int checkpointAmt;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Player player = other.GetComponent<Player>();

            if(player.checkpointIndex == checkpointAmt)
            {
                player.checkpointIndex = 0;
                player.lapNumber++;

                Debug.Log("Lap: "+player.lapNumber);
            }
        }
    }
}
