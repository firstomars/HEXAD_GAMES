using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
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
        anim.SetBool("IsIdle", true);
        //Debug.Log("idling");
    }
}
