using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.NavMeshTest
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        [SerializeField] LayerMask whatIsGround;
        [SerializeField] private Transform[] waypoints;
        //private Transform[] path;
        private List<Vector3> path;

        private bool pathFound;

        private Vector3 currentPosition;
        private Vector3 currentDestination;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach(var waypoint in waypoints)
                {
                    if (waypoint.name == "kitchen")
                    {
                        Debug.Log("kitchen");
                        agent.SetDestination(waypoint.position);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.name == "gym")
                    {
                        Debug.Log("gym");
                        agent.SetDestination(waypoint.position);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.name == "bathroom")
                    {
                        Debug.Log("bathroom");
                        agent.SetDestination(waypoint.position);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.name == "bedroom")
                    {
                        Debug.Log("bedroom");
                        agent.SetDestination(waypoint.position);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.name == "livingroom")
                    {
                        Debug.Log("livingroom");
                        agent.SetDestination(waypoint.position);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.name == "trophycabinet")
                    {
                        Debug.Log("trophycabinet");
                        agent.SetDestination(waypoint.position);
                    }
                }
            }
        }

        //private void FindPath()
        //{
        //    if (currentDestination != null)
        //    {
        //        currentPosition = gameObject.transform.position;

        //        foreach (var waypoint in waypoints)
        //        {
        //            float distCurrentPosToWaypoint = Vector3.Distance(currentPosition, waypoint.position);
        //            float distWayPointToDestination = Vector3.Distance(waypoint.position, currentDestination);
        //            float distCurrentPosToDestination = Vector3.Distance(currentPosition, currentDestination);

        //            if (currentDestination == waypoint.position)
        //            {
        //                path.Add(waypoint.position);
        //                pathFound = true;
        //                return;
        //            }
        //            else if (distCurrentPosToWaypoint < 10 && distWayPointToDestination < distCurrentPosToDestination)
        //            {
        //                path.Add(waypoint.position);
        //            }
        //        }
        //    }

        //}

        private void MoveAgent()
        {
            agent.SetDestination(currentDestination);
        }
    }
}

