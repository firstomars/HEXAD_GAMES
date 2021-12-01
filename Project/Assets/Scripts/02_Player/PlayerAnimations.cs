using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void IdleToWalk()
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalking", true);
        Debug.Log("walking");
    }

    public void WalkToIdle()
    {
        //Debug.Log(anim);

        anim.SetBool("IsIdle", true);
        anim.SetBool("IsWalking", false);
        Debug.Log("idling");
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
        anim.SetBool("IsInGym", true);
    }

    public void OutsideGym()
    {
        anim.SetBool("IsInGym", false);
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
        anim.SetTrigger("Eat");
    }

    public void EatJunkFood()
    {
        Debug.Log("Eat junk food animation");
        
        anim.SetTrigger("Eat");
    }

    public void PlayMinigame()
    {
        anim.SetBool("IsPlaying", true);
         //anim.SetTrigger("PlayGame");
        Debug.Log("Minigame animation");
    }

    public void StopMinigame()
    {
        anim.SetBool("IsPlaying", false);       
        Debug.Log("Exit Minigame animation");
    }

    public void SitDown()
    {
        anim.SetBool("IsSitting", true);

    }

    public void SitUp()
    {
        anim.SetBool("IsSitting", false);
    }
}



