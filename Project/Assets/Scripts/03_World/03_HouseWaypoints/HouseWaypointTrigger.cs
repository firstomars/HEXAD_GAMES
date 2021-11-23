using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseWaypointTrigger : MonoBehaviour
{
    private PlayerAnimations PlayerAnimations;

    private void Awake()
    {
        PlayerAnimations = GameManager.Instance.player.GetComponent<PlayerAnimations>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.AudioManagerInstance.StopSound("FootStep");
        PlayerAnimations.WalkToIdle();
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.AudioManagerInstance.PlaySound("FootStep");
        PlayerAnimations.IdleToWalk();
    }
}
