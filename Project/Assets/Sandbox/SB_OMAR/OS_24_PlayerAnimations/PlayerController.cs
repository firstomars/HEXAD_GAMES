using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Sandbox.Omar.PlayerAnimations
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;

        Animator m_Animator;

        [SerializeField] private Transform[] waypoints;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            m_Animator = gameObject.GetComponent<Animator>();
        }

        private void Update()
        {

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                agent.SetDestination(waypoints[0].position);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                agent.SetDestination(waypoints[0].position);
            }


            //if (Input.GetMouseButtonDown(0))
            //{
            //    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hitInfo;

            //    if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
            //        agent.SetDestination(hitInfo.point);
            //}
        }

        public void IdleToWalk()
        {
            m_Animator.SetBool("IsIdle", false);
            Debug.Log("walking");
        }

        public void WalkToIdle()
        {
            m_Animator.SetBool("IsIdle", true);
            Debug.Log("idling");
        }
    }

}
