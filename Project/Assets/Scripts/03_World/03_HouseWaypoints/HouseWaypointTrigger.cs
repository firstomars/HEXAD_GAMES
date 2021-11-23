using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseWaypointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.AudioManagerInstance.StopSound("FootStep");
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.AudioManagerInstance.PlaySound("FootStep");
    }
}
