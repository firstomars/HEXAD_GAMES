using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace Sandbox.Omar.SleepTest
{
    public class PlayerController : MonoBehaviour
    {
        NavMeshAgent agent;
        [SerializeField] LayerMask whatIsPlayer, whatIsGround, TEST_GoToSleep;
        [SerializeField] private bool isBedTime = false;
        [SerializeField] private bool isWakeUpTime = false;
        [SerializeField] private Transform bedPos;
        [SerializeField] private Transform reportPos;
        [SerializeField] private GameObject reportObj;
        private Text report;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            reportObj.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            float dist = agent.remainingDistance;

            if (isBedTime && isWakeUpTime)
            {
                isBedTime = false;
                agent.SetDestination(reportPos.position);
            }
            else if (isWakeUpTime)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            reportObj.SetActive(true);
                        }
                    }
                }
            }
            else if (isBedTime)
            {
                agent.SetDestination(bedPos.position);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, TEST_GoToSleep)) //NOT PICKING UP LAYER MASK
                {
                    if (hitInfo.collider.tag == "TESTBTN") isBedTime = true;
                }
                else if (!isBedTime && Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
                    agent.SetDestination(hitInfo.point);
            }
        }

        public void CloseButton()
        {
            reportObj.SetActive(false);
            isWakeUpTime = false;
        }

    }

}
