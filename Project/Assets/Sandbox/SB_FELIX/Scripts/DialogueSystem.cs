using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    //=== Comment Craig
    // do these variables need to be public? Are they accessed outside this class?
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;


    [System.Serializable]
    public class Tips
    {
        public string tipType;
        public string[] tipText;
        
    }
    //=== Comment Craig
    // while no declaration will make this private, its good practice to declare
    [SerializeField] Tips[] tips;


    [System.Serializable]
    public class Introduction
    {
        public string Intro;
        public string[] IntroText;

    }
    //=== Comment Craig
    // pascal case for local variables e.g. introDialogue
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

    //=== Comment Craig
    // Will this need to be called from outside your class, declare as publci if that is the case
    // By default all variable and method declrations are private, but declarinf specifically adds to readability.
    // e.g. private void StartDialogue()
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    //=== Comment Craig
    // What if the passed in text string would overflow the useable text space? Do you need a continue function or overflow protection in here?
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
