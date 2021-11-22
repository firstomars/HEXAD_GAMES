using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.AnimationTesting
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator anim;
        private bool anim_isBouncing = false;
        private bool anim_isTilting = false;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim_isBouncing = !anim_isBouncing;
                anim.SetBool("isBouncing", anim_isBouncing);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                anim_isTilting = !anim_isTilting;
                anim.SetBool("isTilting", anim_isTilting);
            }
        }
    }

}
