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
        [SerializeField] private bool isInBedroom = false;
        [SerializeField] private bool isBedTime = false;
        [SerializeField] private bool isWakeUpTime = false;
        [SerializeField] private Transform bedPos;

        [Header("UI")]
        [SerializeField] private Transform reportPos;
        [SerializeField] private GameObject reportObj;
        [SerializeField] private GameObject sleepBtn;
        [SerializeField] private GameObject wakeUpBtn;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            reportObj.SetActive(false);
            sleepBtn.SetActive(false);
            wakeUpBtn.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            float dist = agent.remainingDistance;

            if (isInBedroom)
            {
                if (!isBedTime)
                {
                    sleepBtn.SetActive(true);
                    wakeUpBtn.SetActive(false);
                }
                else
                {
                    sleepBtn.SetActive(false);
                    wakeUpBtn.SetActive(true);
                }
            }
            else
            {
                sleepBtn.SetActive(false);
                wakeUpBtn.SetActive(false);
            }

            if (isBedTime && isWakeUpTime)
            {
                isBedTime = false;
                agent.SetDestination(reportPos.position);
            }
            else if (isWakeUpTime)
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
                {
                    reportObj.SetActive(true);
                }

                //DELETE WHEN FULLY TESTED
                /*
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
                */
            }
            else if (isBedTime)
            {
                agent.SetDestination(bedPos.position);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
                    agent.SetDestination(hitInfo.point);
            }
        }

        //===
        //PLAYERCONTRLLER
        public void IsInBedroom(bool value)
        {
            isInBedroom = value;
        }
        //===

        //===
        //UI & PLAYERCONTROLLER CONNECTION

        //player manually sends pet to bed
        public void SendToBed()
        {
            isBedTime = true;
        }

        //player manually wakes pet up
        public void WakeUp()
        {
            isWakeUpTime = true;
        }

        //close report
        public void CloseButton()
        {
            reportObj.SetActive(false);
            isWakeUpTime = false;
        }

        //===

    }

}
