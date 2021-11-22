using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.AnimationTesting
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;
        private Vector3 currentDestination;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
                {
                    currentDestination = hitInfo.point;
                    agent.SetDestination(currentDestination);
                }
            }
        }
    }
}
