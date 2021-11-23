using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.PlayerAnimations
{
    public class HouseWaypointTrigger : MonoBehaviour
    {
        [SerializeField] GameObject PCObj;
        private PlayerController PC;

        private void Start()
        {
            PC = PCObj.GetComponent<PlayerController>();
            Debug.Log(PC);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger on");
            PC.WalkToIdle();
            //AudioManager.AudioManagerInstance.StopSound("FootStep");
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("trigger off");
            PC.IdleToWalk();
            //AudioManager.AudioManagerInstance.PlaySound("FootStep");
        }
    }

}
