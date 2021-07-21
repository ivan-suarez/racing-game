using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckpoint : MonoBehaviour
{

    public int index;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Player player = other.GetComponent<Player>();
           
            
            

            if (player.checkpointIndex == index + 1 || player.checkpointIndex == index - 1)
            {
                player.checkpointIndex = index;
                
            }
        }

    }
}
