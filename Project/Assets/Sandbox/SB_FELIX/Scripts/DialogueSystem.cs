using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;


    [System.Serializable]
    public class Tips
    {
        public string tipType;
        public string[] tipText;
        //public Tuple<Transform, int>[] enemyPrefabCount;???
        
    }
    [SerializeField] Tips[] tips;


    [System.Serializable]
    public class Introduction
    {
        public string Intro;
        public string[] IntroText;

    }
    [SerializeField] Introduction[] Intro2;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else 
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    void NextLine()
    {
        if (index <lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
