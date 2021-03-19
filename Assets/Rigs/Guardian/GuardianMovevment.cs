using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianMovevment : MonoBehaviour
{
    public Animator animMachine;

    void Start()
    {
        animMachine = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Vertical"); // - 1

        animMachine.SetFloat("current speed", speed);

        transform.position += transform.forward * speed * Time.deltaTime * 3;

    }
}
