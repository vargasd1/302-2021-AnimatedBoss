using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    
    NavMeshAgent agent;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        

    }

    // Update is called once per frame
    void Update()
    {
        float dis=Vector3.Distance(transform.position, player.transform.position);

         if(dis > 10)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

    }
}
