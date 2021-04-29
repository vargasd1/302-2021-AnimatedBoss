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






    private Camera cam;

    public States state { get; private set; }
    public Vector3 moveDir { get; private set; }

    void Start()
    {
        state = States.Idle;
        pawn = GetComponent<CharacterController>();

        cam = Camera.main;
    }

    
    void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        bool isTryingToMove = (horz != 0 || vert != 0);

        if (isTryingToMove)
        {
            float camYaw = cam.transform.eulerAngles.y;
            transform.rotation = AnimMath.Slide(transform.rotation, Quaternion.Euler(0, camYaw, 0), .02f);
        }



        moveDir = transform.forward * vert + transform.right * horz;
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

        if (pawn.isGrounded)
        {
            if(moveDir.sqrMagnitude > 0.1f)
            {
                state = States.Walk;

            }
            else
            {
                state = States.Idle;
            }
        }
    }
}
