using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.UITesting
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;

        [SerializeField] private Transform[] houseWaypoints;
        [SerializeField] private GameObject UIScriptObj;
        private UIScript UIScript;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            UIScript = UIScriptObj.GetComponent<UIScript>();

            UIScript.SetNavigationUIListeners(this);
        }

        private void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hitInfo;

            //    if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
            //        agent.SetDestination(hitInfo.point);
            //}
        }

        public void SetPlayerDestination(Vector3 targetPos)
        {
            agent.SetDestination(targetPos);
            gameObject.transform.LookAt(agent.steeringTarget);
        }


        public Vector3 FindWaypointHelper(string room)
        {
            foreach (var waypoint in houseWaypoints)
            {
                if (waypoint.name == room) return waypoint.position;
            }
            return Vector3.zero;
        }

        public void SeekKitchen()
        {
            Debug.Log("seeking kitchen");
            SetPlayerDestination(FindWaypointHelper("kitchen"));
        }

        public void SeekGym()
        {
            Debug.Log("seeking gym");
            SetPlayerDestination(FindWaypointHelper("gym"));
        }

        public void SeekBathroom()
        {
            Debug.Log("seeking bathroom");
            SetPlayerDestination(FindWaypointHelper("bathroom"));
        }

        public void SeekBedroom()
        {
            Debug.Log("seeking bedroom");
            SetPlayerDestination(FindWaypointHelper("bedroom"));
        }

        public void SeekTrophyCabinet()
        {
            Debug.Log("seeking trophy cabinet");
            SetPlayerDestination(FindWaypointHelper("trophycabinet"));
        }

        public void SeekLivingRoom()
        {
            Debug.Log("seeking living room");
            SetPlayerDestination(FindWaypointHelper("livingroom"));
        }
    }
}
