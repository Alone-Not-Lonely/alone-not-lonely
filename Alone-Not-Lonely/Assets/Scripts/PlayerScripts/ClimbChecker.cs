﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    public float checkStep = .1f, reachHeight = 0,
                 climbLengthDepth = 1.5f, maxClimbHeight = 2, 
                 handOffset = .3f, handMoveSpeed = .3f,
                 detectRadius = 1f;
    private float reachDepth = 1f;
    private float playerHeight;
    public Vector3 climbablePoint = Vector3.zero;
    private Vector3 edge;
    public Transform lhand, rhand;
    private Transform pTransform;
    private CharacterController pControl;
    private PlayerAbilityController pAbil;
    private PlayerMovementController pMC;
    

    //Scripts using this will check if Vector3.zero
    //So lets just be sure not to have any climbable places at origin

    // Start is called before the first frame update
    void Start()
    {
        edge = transform.position;
        pAbil = GetComponentInParent<PlayerAbilityController>();
        pControl = GetComponentInParent<CharacterController>();
        pTransform = GetComponentInParent<Transform>();
        playerHeight = GetComponentInParent<CharacterController>().height;
        pMC = GetComponentInParent<PlayerMovementController>();
    }

    private void FixedUpdate()
    {
        if (pControl.isGrounded) { adjustHeight(); }
        updateHands();
    }

    //should be a coroutine for performance, but testing the idea first. 
    public void adjustHeight()
    {
        RaycastHit proxHit;
        Ray proxRay = new Ray(transform.position, transform.forward);
       
        Debug.DrawRay(transform.position, (transform.forward*reachDepth), Color.blue);
        if (Physics.SphereCast(proxRay,detectRadius, out proxHit, reachDepth) && (proxHit.collider.isTrigger == false))//Raycast(proxRay, out proxHit, reachDepth))
        {
            RaycastHit canLandHit;
            Vector3 hcPos = transform.position + (transform.up * reachHeight);

            Ray landingRay = new Ray(hcPos, transform.forward * climbLengthDepth);
            //Test:
            Debug.DrawRay(landingRay.origin, landingRay.direction, Color.yellow);

            if (Physics.Raycast(landingRay, out canLandHit, climbLengthDepth) && (canLandHit.collider.isTrigger == false))
            {
                
                if (reachHeight < maxClimbHeight)
                {
                    reachHeight += checkStep;//raises the reach ever so slightly
                }
            }
            else
            {//We are close enough and HAVE found the objects top 
                
                edge = new Vector3(transform.position.x + landingRay.direction.x,
                                   transform.position.y + reachHeight,
                                   transform.position.z + landingRay.direction.z);

                climbablePoint = new Vector3(edge.x , edge.y + (playerHeight * 1.3f), edge.z )+transform.forward*climbLengthDepth;                           
            }

        }
        else
        {//We are not close to an object
            //Reset reach height and climbable point
            reachHeight = 0;
            climbablePoint = Vector3.zero;
            if (!pMC.climbing)
            {
                edge = transform.position;
            }
        }
    }

    
    private void updateHands()
    {
        {
            rhand.position = Vector3.Lerp(rhand.position, edge + (transform.right * handOffset), handMoveSpeed);
            lhand.position = Vector3.Lerp(lhand.position, edge - (transform.right * handOffset), handMoveSpeed);
        }
        if (Vector3.Distance(rhand.position, edge) < .1)
        {
            //hand closed indicator
        }
    }


    private float posDiff()
    {
        return (transform.position.y - pTransform.position.y);
    }
}
