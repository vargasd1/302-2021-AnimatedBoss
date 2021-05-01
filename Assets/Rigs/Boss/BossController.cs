using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
     
    NavMeshAgent agent;
    public GameObject player;

    float health = 200;


    public States state;
    

    public enum States
    {
        Idle,
        Walk,
        Attack,
        Death
    }



    
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        state = States.Walk;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(state == States.Idle)
        {

        }


        if (state == States.Walk)
        {
            float dis = Vector3.Distance(transform.position, player.transform.position);

            if (dis > 10)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
        
    }

    
    

}
