using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walk
    }

    private CharacterController pawn;

    public float walkSpeed = 5;
    public float stepSpeed = 5;

    //public float walkCD = 3;

    public Vector3 walkScale = Vector3.one;

    public AnimationCurve ankleRotCurve;

    public States state { get; private set; }
    public Vector3 moveDir { get; private set; }

    void Start()
    {
        state = States.Idle;
        pawn = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        Vector3 moveDir = transform.forward * vert + transform.right * horz;
        if(moveDir.sqrMagnitude > 1) moveDir.Normalize();
        /*if(vert > 0|| horz >0 )
        {
            state= States.Walk;
        }*/
        pawn.SimpleMove(moveDir * walkSpeed);

        /*walkCD--;
        if(walkCD < 0)
        {
            state = States.Idle;
        }*/

    }
}
