using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Omar.WanderBehaviour
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;

        [SerializeField] private Transform[] houseWaypoints;
        [SerializeField] private GameObject UIScriptObj;
        private UIScript UIScript;

        private bool isWandering = false;
        private bool isWanderDestinationSet = false;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            UIScript = UIScriptObj.GetComponent<UIScript>();

            UIScript.SetNavigationUIListeners(this);
        }

        private void Update()
        {
            if (Vector3.Distance(agent.destination, gameObject.transform.position) < 3.0f)
            {
                Debug.Log("agent reached destination");
                StartCoroutine(WaitBeforeWander());
            }
            else
            {
                isWandering = false;
            }

            if (isWandering && !isWanderDestinationSet)
            {
                SetWanderDestination();
                isWanderDestinationSet = true;
            }
        }

        private void SetWanderDestination()
        {
            int min = 0;
            int max = houseWaypoints.Length;

            System.Random randomNum = new System.Random();

            int randomIndex = randomNum.Next(min, max);

            SetPlayerDestination(houseWaypoints[randomIndex].position);
            Debug.Log("random destination set to " + houseWaypoints[randomIndex].name);
        }

        private IEnumerator WaitBeforeWander()
        {
            Debug.Log("wait before wander started");
            yield return new WaitForSeconds(3);
            isWandering = true;
            isWanderDestinationSet = false;
            Debug.Log("wait finished");
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
