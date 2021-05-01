using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    BossController boss;
    PlayerController player;


    void Start()
    {
        boss = GetComponentInParent<BossController>();
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


    private void OnTriggerStay(Collider other)
    {
        boss.state = BossController.States.Attack;
        PlayerController player = other.GetComponent<PlayerController>();
        HealthSystem health = other.GetComponent<HealthSystem>();

        if(true)
        {
            health.TakeDamage(20, 5);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        boss.state = BossController.States.Walk;
    }
}
