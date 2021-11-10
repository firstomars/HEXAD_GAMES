using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Sandbox.Omar.DialogueTest
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueSysGO;
        private DialogueSystem DialogueSystem;

        [SerializeField] LayerMask whatIsGround, whatIsPlayer, whatIsUIPrompt;
        private NavMeshAgent agent;

        private bool isInBedroom;
        private bool isInGym;

        private bool isTextActive;

        private bool isTipDelivered;

        // Start is called before the first frame update
        void Start()
        {
            DialogueSystem = dialogueSysGO.GetComponent<DialogueSystem>();
            agent = GetComponent<NavMeshAgent>();

            isInBedroom = false;
            isInGym = false;
            isTipDelivered = false;
        }

        public void SetPlayerPosition(string room = default)
        {
            switch (room)
            {
                case "bedroom":
                    Debug.Log("player in bathroom");
                    isInBedroom = true;
                    isInGym = false;
                    break;

                case "gym":
                    Debug.Log("player in gym");
                    isInBedroom = false;
                    isInGym = true;
                    break;

                default:
                    Debug.Log("player in house");
                    isInBedroom = false;
                    isInGym = false;
                    isTipDelivered = false;
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isTextActive)
            {
                if (!isTextActive && Input.GetMouseButtonDown(0))
                {
                    MovePlayer();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    DialogueSystem.AccessLine();
                }
            }
            else if (isTextActive && Input.GetKeyDown(KeyCode.Space))
            {
                DialogueSystem.CloseTip();
                isTextActive = false;
                isTipDelivered = true;
            }

            //access tips
            if (isInBedroom && !isTipDelivered)
            {
                DialogueSystem.AccessTip("BEDROOM");
                isTipDelivered = true;
                isTextActive = true;
            }
            else if (isInGym && !isTipDelivered)
            {
                DialogueSystem.AccessTip("GYM");
                isTipDelivered = true;
                isTextActive = true;
            }
        }

        private void MovePlayer()
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, whatIsGround))
                agent.SetDestination(hitInfo.point);
        }
    }
}
