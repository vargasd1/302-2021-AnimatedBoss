using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyStub : MonoBehaviour
{
    public Transform stepPosition;

    public AnimationCurve vertStepMovement;

    private Vector3 prevPlantedPos;
    private Quaternion prevPlantedRot = Quaternion.identity;

    private Vector3 plantedPos;
    private Quaternion plantedRot = Quaternion.identity;


    private float time = .25f;
    private float currTime = 0;



    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (CheckIfCanStep())
        {
            DoRaycast();
        }

        if(currTime < time) // anim IS playing
        {
            currTime += Time.deltaTime; // move forward 

            float p = currTime / time;


           Vector3 finalPos = transform.position = AnimMath.Lerp(prevPlantedPos, plantedPos, p);

            finalPos.y += vertStepMovement.Evaluate(p) * 2;

            transform.position = finalPos;

            transform.rotation = AnimMath.Lerp(prevPlantedRot, plantedRot, p);
        }
        else
        { // anim is NOT playing
            transform.position = plantedPos;
            transform.rotation = plantedRot;
        }

        

    }

    bool CheckIfCanStep()
    {
        Vector3 vBetween = transform.position - stepPosition.position;
        float threashold = 7;


        return (vBetween.sqrMagnitude > threashold * threashold);
    }

    void DoRaycast()
    {
        Ray ray = new Ray(stepPosition.position + Vector3.up, Vector3.down);

        Debug.DrawRay(ray.origin, ray.direction * 5);

        if(Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            // beginning anim setup
            prevPlantedPos = transform.position;
            prevPlantedRot = transform.rotation;

            // ending anim setup
            plantedPos = hit.point;
            plantedRot = Quaternion.FromToRotation(-transform.up, hit.normal);


            // start anim
            currTime = 0;
        }
    }
}
