using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    //public ParticleSystem prefabExplosion;
    [SerializeField] public float health { get; private set; } // public for unity, private for other scripts 

    public float healthMax = 100;

    public float iFrames = 0;
    

    private void Start()
    {
        health = healthMax;
    }


    private void Update()
    {
        if (iFrames >= 0) iFrames -= Time.deltaTime;
        //print(health);
    }



    public void TakeDamage(float amt, float iFrms)
    {
        if (amt <= 0) return;
        if (iFrames > 0) return; 


        health -= amt;
        iFrames = iFrms;
              
        print(health);


        if (health <= 0) Die();

        
    }

    public void Die()
    {
        // removes this gameobject from the game
        Destroy(gameObject);
       // Instantiate(prefabExplosion, gameObject.transform.position, gameObject.transform.rotation);
        

    }
}
