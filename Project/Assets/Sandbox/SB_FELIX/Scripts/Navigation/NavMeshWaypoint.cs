using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavMeshWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;
    public NavMeshAgent agent;
    public int num = 0;

    public float minimumDistance;
    public float speed;

    public bool random = false;
    public bool start = true;

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);

        if (start)

        {
            if (dist > minimumDistance && Input.GetKeyDown(KeyCode.Space))
            {
                MoveCharacter();
            }

            else
            {
                if (!random) //If random is not checked in the inspector
                {
                    if (num + 1 == waypoints.Length) //See if this is the last waypoint in the array
                    {
                        num = 0; //If it is the last waypoint, set to 0
                    }
                    else
                    {
                        num++; // If it isn't, adds 1
                    }
                }
                else
                {
                    num = Random.Range(0, waypoints.Length);

                }
            }


        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IdleToWalk();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetIntoBed();
            m_Animator.SetBool("IsIdle", false);
        }



        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Workout();
        }
    }




    private void OnTriggerEnter(Collider other) 
    {
        m_Animator.SetBool("IsIdle", true);
        InsideGym();
        
    }

    public void IdleToWalk() 
    {
        m_Animator.SetBool("IsIdle", false);
    }

    public void WalkToIdle()
    {
        m_Animator.SetBool("IsIdle", true);
    }

    public void GetIntoBed()
    {
        m_Animator.SetBool("IntoBed", true);      
    }


    public void GetOutOfBed()
    {
        m_Animator.SetTrigger("GetOutOfBed");
        m_Animator.SetBool("IsIdle", true);
        m_Animator.SetBool("IntoBed", false);
    }

    public void InsideGym()
    {
        m_Animator.SetBool("InsideGym", true);
    }

    public void Workout()
    {
        m_Animator.SetTrigger("Workout");
    }


    public void MoveCharacter()
    {
        gameObject.transform.LookAt(waypoints[num].transform.position); //Rotating the player towards the position
        agent.destination = waypoints[num].transform.position;

        
    }
}
