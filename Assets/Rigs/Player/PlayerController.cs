using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walk,
        Death,
        Attack
    }

    private CharacterController pawn;

    public float walkSpeed = 5;
    public float stepSpeed = 5;

    public GameObject projectile;
    public GameObject player;
    public Transform magicSpawnLoc;
    

    //public float walkCD = 3;

    public Vector3 walkScale = Vector3.one;

    public AnimationCurve ankleRotCurve;


    private Camera cam;

    public States state;

    

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


       
        pawn.SimpleMove(moveDir * walkSpeed);

       

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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                state = States.Attack;

                GameObject PAttack = Instantiate(projectile, magicSpawnLoc.position, player.transform.rotation, null) as GameObject;
                Rigidbody rb = PAttack.GetComponent<Rigidbody>();
                rb.velocity = player.transform.forward * 20;
                


            }
        }
    }
}
