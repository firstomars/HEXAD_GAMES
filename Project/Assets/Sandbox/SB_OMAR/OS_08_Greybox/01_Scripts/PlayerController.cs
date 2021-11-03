using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.Greybox
{
    public class PlayerController : MonoBehaviour
    {
        [Header("NavMesh")]
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;

        [Header("Cameras")]
        [SerializeField] private GameObject camMgr;
        private CameraManager CameraManager;

        //navigation variables
        private Vector3 currentTarget;
        private bool isTargetReached = true;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            if (camMgr == null) 
                camMgr = GameManager.Instance.world.transform.GetChild(0).gameObject;
            
            CameraManager = camMgr.GetComponent<CameraManager>();
        }

        // Update is called once per frame
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

            if (!isTargetReached && Vector3.Distance(gameObject.transform.position, currentTarget) < 2.0f)
            {
                isTargetReached = true;
                CameraManager.SwitchCamera();
            }

        }
    }

}