using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider hit)
    {
        HealthSystem health = hit.gameObject.GetComponent<HealthSystem>();
        BossController boss = hit.gameObject.GetComponent<BossController>();

        if(boss != null && health != null)
        {
            health.TakeDamage(10, 0);
            Destroy(gameObject);
        }
    }
}
