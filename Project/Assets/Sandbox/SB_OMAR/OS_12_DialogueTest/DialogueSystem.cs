using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sandbox.Omar.DialogueTest
{
    public class DialogueSystem : MonoBehaviour
    {
        public TextMeshProUGUI lineTextComponent; //renamed to align with gameobject
        public string[] lines;
        public float textSpeed;

        private int index;

        //===
        //NEW
        [SerializeField] private GameObject lineCanvas;
        private bool isLineTextActive;

        [SerializeField] private GameObject tipCanvas;
        [SerializeField] private TextMeshProUGUI tipTextComponent;
        private bool isTipTextActive;
        //===

        [System.Serializable]
        public class Tip // made singular
        {
            public string tipType;
            public string[] tipText;
            
        }
        [SerializeField] Tip[] tips; //array is plural


        [System.Serializable]
        public class Introduction
        {
            public string Intro;
            public string[] IntroText;
        }
        [SerializeField] Introduction[] IntroDialogue;

        [System.Serializable]
        public class PetDialogue
        {
            public string Dialogue;
            public string[] PetText;

        }
        [SerializeField] PetDialogue[] petDialogue;

        void Start()
        {
            lineTextComponent.text = string.Empty;

            //===
            //NEW
            isLineTextActive = false;
            isTipTextActive = false;
            //===

            //OLD
            //StartDialogue();
        }

        void Update()
        {
            if (!isLineTextActive) SetLineGameobject(false);
            if (!isTipTextActive) SetTipGameobject(false);


            //OLD UPDATE LOGIC
                /*
                if(Input.GetMouseButtonDown(0))
                {
                    if(textComponent.text == lines[index]) // what does this mean??
                    {
                        NextLine();
                    }
                    else 
                    {
                        StopAllCoroutines(); //explain this
                        textComponent.text = lines[index];
                    }
                }
                */
        }

        private void SetLineGameobject(bool value)
        {
            lineCanvas.SetActive(value);
        }

        private void SetTipGameobject(bool value)
        {
            tipCanvas.SetActive(value);
        }

        public void StartDialogue()
        {
            //set dialogue to active
            isLineTextActive = true;
            SetLineGameobject(isLineTextActive);

            index = 0;
            StartCoroutine(TypeLine());
        }

      
        public void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                lineTextComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                //gameObject.SetActive(false);
                isLineTextActive = false;
                lineTextComponent.text = string.Empty;
            }
        }


        //accessed by playercontroller class
        public void AccessLine()
        { 
            if (isLineTextActive) NextLine();
            else StartDialogue();
        }

        public void AccessTip(string type = default)
        {
            if (isTipTextActive)
            {
                isTipTextActive = false;
                SetTipGameobject(isTipTextActive);
            }
            else
            {
                foreach (Tip t in tips)
                {
                    if (t.tipType == type)
                        DeliverRandomTip(t);
                }
            }
        }


        private void DeliverRandomTip(Tip tipType)
        {
            tipTextComponent.text = tipType.tipText[GetRandomNumber(0, tipType.tipText.Length)];
            isTipTextActive = true;
            SetTipGameobject(isTipTextActive);
        }

        public void CloseTip()
        {
            tipTextComponent.text = string.Empty;
            isTipTextActive = false;
            SetTipGameobject(isTipTextActive);
        }


        public int GetRandomNumber(int min, int max)
        {
            System.Random rnd = new System.Random();

            return rnd.Next(min, max);
        }



        IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                lineTextComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

    }
}
