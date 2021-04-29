using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script animates the foot and legs by 
/// changing the local position of this object (ik target).
/// </summary>
public class FeetAnim : MonoBehaviour
{
    /// <summary>
    /// The local-space starting position of this object
    /// </summary>
    private Vector3 startingPos;


    /// <summary>
    /// The local-space starting rotation of this object
    /// </summary>
    private Quaternion startingRot;

    /// <summary>
    /// An offset value to use for timing of the Sin wave that 
    /// controls the walk animation. Measured in radians.
    /// 
    /// A value of Mathf.PI would be half-a-period.
    /// </summary>
    public float stepOffset = 0;

    PlayerController player;

    private Vector3 targetPos;
    private Quaternion targetRot;

    void Start()
    {
        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
        player = GetComponentInParent<PlayerController>();
    }


    void Update()
    {
        print(player.state);
        switch (player.state)
        {
            case PlayerController.States.Idle:
                AnimateIdle();
                break;
            case PlayerController.States.Walk:
                AnimateWalk();
                break;
        }
        // ease position and rotatoin towards their targets:

        //transform.position = AnimMath.Slide(transform.position, targetPos, .01f);
        //transform.rotation = AnimMath.Slide(transform.rotation, targetRot, .01f);

    }

    void AnimateWalk()
    {
        Vector3 finalPos = startingPos;

        float time = (Time.time + stepOffset) * player.stepSpeed;

        // lateral movement:(z + x)
        float frontToBack = Mathf.Sin(time);

        //finalPos += player.moveDir * frontToBack * player.walkScale.z;
        if (Input.GetKey(KeyCode.W)) finalPos.z += frontToBack * player.walkScale.z;
        else if (Input.GetKey(KeyCode.S)) finalPos.z -= frontToBack * player.walkScale.z;

        if (Input.GetKey(KeyCode.A)) finalPos.x -= frontToBack * player.walkScale.z * 0.8f;
        else if (Input.GetKey(KeyCode.D)) finalPos.x += frontToBack * player.walkScale.z * 0.8f;


        //vertical movement: (y)
        finalPos.y += Mathf.Cos(time) * player.walkScale.y;


        bool isOnGround = (finalPos.y < startingPos.y);

        if (isOnGround) finalPos.y = startingPos.y;

        // convert from z ( -1 to 1) to p (0 to 1 to 0)
        float p = 1 - Mathf.Abs(frontToBack);

        float anklePitch = isOnGround ? 0 : -p * 20;

        finalPos = new Vector3(finalPos.x, finalPos.y - 1.25f, finalPos.z);

        transform.localPosition = finalPos;
        transform.localRotation = startingRot * Quaternion.Euler(0, 0, anklePitch);

        //targetPos = transform.TransformPoint(finalPos);
        // targetRot = transform.parent.rotation * startingRot * Quaternion.Euler(0, 0, anklePitch);
    }

    void AnimateIdle()
    {
        transform.localPosition = startingPos;
        transform.localRotation = startingRot;

        //targetPos = transform.TransformPoint(startingPos);
        // targetRot = transform.parent.rotation * startingRot;

        FindGround();
    }
    void FindGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, .5f, 0), Vector3.down * 2);

        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.point = new Vector3(hit.point.x, hit.point.y + .75f, hit.point.z);
            transform.position = hit.point;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            //targetPos = hit.point;
            //targetRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        }
        else
        {

        }
    }

}


