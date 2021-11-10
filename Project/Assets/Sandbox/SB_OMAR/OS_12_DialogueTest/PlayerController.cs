using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.DialogueTest
{
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueSysGO;
        private DialogueSystem DialogueSystem;

        // Start is called before the first frame update
        void Start()
        {
            DialogueSystem = dialogueSysGO.GetComponent<DialogueSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                DialogueSystem.AccessLine();

            if (Input.GetMouseButtonDown(1))
                DialogueSystem.AccessTip("EXERCISING");
        }
    }
}
