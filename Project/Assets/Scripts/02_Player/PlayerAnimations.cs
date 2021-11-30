using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void IdleToWalk()
    {
        anim.SetBool("IsIdle", false);
        //Debug.Log("walking");
    }

    public void WalkToIdle()
    {
        //Debug.Log(anim);

        anim.SetBool("IsIdle", true);
        //Debug.Log("idling");
    }

    public void GetIntoBed()
    {
        anim.SetBool("IntoBed", true);
        anim.SetBool("IsIdle", false);
    }

    public void GetOutOfBed()
    {
        anim.SetBool("IsIdle", true);
        anim.SetBool("IntoBed", false);
    }

    public void InsideGym()
    {
        anim.SetBool("InsideGym", true);
    }  

    public void Workout()
    {
        Debug.Log("Workout animation");
        anim.SetBool("IsLifting", true);
        //anim.SetTrigger("Workout");
    }

    public void Eat()
    {
        Debug.Log("Eat animation");
        anim.SetBool("IsEating", true);
    }

    public void EatJunkFood()
    {
        Debug.Log("Eat junk food animation");
        
        //anim.SetTrigger("Eat");
    }

    public void PlayMinigame()
    {
        Debug.Log("Minigame animation");
        //anim.SetTrigger("Eat");
    }
}
