using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.CameraSetup
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Cameras")]
        [SerializeField] private GameObject camMgr;
        private CameraManager CameraManager;
        
        [Header("NavMesh")]
        private NavMeshAgent agent;
        [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

        //navigation variables
        private Vector3 currentTarget;
        private bool isTargetReached = true;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            CameraManager = camMgr.GetComponent<CameraManager>();
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
                    currentTarget = hitInfo.point;
                    isTargetReached = false;
                }
            }

            if (!isTargetReached && Vector3.Distance(gameObject.transform.position, currentTarget) < 1.0f)
            {
                isTargetReached = true;
                CameraManager.SwitchCamera();
            }
        }
    }

}
