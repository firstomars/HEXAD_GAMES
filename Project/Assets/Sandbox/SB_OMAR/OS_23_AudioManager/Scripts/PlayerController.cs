using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.AudioManager
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] LayerMask whatIsPlayer, whatIsGround;

        private NavMeshAgent agent;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
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