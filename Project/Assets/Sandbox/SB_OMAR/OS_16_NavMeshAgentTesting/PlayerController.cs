using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.NavMeshAgentTesting
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;

        private void Awake()
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
                    agent.SetDestination(hitInfo.point);
            }
        }
    }
}
