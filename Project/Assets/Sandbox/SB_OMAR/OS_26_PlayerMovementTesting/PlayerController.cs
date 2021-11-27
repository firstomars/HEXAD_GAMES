using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.PlayerMovementTesting
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;

        private PlayerAnimations PlayerAnimations;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            PlayerAnimations = GetComponent<PlayerAnimations>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
                {
                    agent.SetDestination(hitInfo.point);
                }
            }

            if (agent.remainingDistance > (agent.stoppingDistance + 0.1f))
            {
                Debug.Log("idle to walk");
                PlayerAnimations.IdleToWalk();
            }
            else
            {
                PlayerAnimations.WalkToIdle();
                FaceTarget(Camera.main.transform.position);
            }
        }

        private void FaceTarget(Vector3 destination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
        }
    }

}
