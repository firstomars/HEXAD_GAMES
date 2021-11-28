using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sandbox.Omar.TMProTesting
{
    public class TextUpdate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        // Start is called before the first frame update
        void Start()
        {
            text = text.GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) text.text = "hello world";
        }
    }

}