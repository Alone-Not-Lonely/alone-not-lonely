﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    public float checkStep = .1f, reachHeight = 0,
                 climbLengthDepth = 1.5f, maxClimbHeight = 2,
                 handOffset = .3f, handMoveSpeed = .3f,
                 detectRadius = 1f, landingDepth = .5f,
                 maxReachDist = 4f;
    public float reachDepth = 1f;
    private float playerHeight;
    public Vector3 climbablePoint = Vector3.zero;
    private Vector3 edge;
    public Transform lhand, rhand;
    private Transform pTransform;
    private CharacterController pControl;
    private PlayerAbilityController pAbil;
    private PlayerMovementController pMC;
    public GameObject climbableObject;

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
        if (Physics.SphereCast(proxRay,detectRadius, out proxHit, reachDepth) && (proxHit.collider.isTrigger == false))
        {
            //Object Gabe could concievably climb
            climbableObject = proxHit.transform.gameObject;

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
               
                Vector3 possEdge = new Vector3(transform.position.x + landingRay.direction.x,
                                  transform.position.y + reachHeight,
                                  transform.position.z + landingRay.direction.z);

                //Can stand ray slightly upward to deal with the awkward shape of boxes
                Ray canStandRay = new Ray((possEdge+Vector3.up + transform.forward*landingDepth), -transform.up);
                Debug.DrawRay(canStandRay.origin, canStandRay.direction, Color.green);

                RaycastHit canStandHit;
                if (Physics.Raycast(canStandRay, out canStandHit) && (canStandHit.collider.isTrigger == false))
                {
                    //Debug.Log("Can stand on: " + canLandHit);
                    edge = possEdge;
                    climbablePoint = new Vector3(edge.x, edge.y + (playerHeight * 1.3f), edge.z) + transform.forward * landingDepth;
                }
                else
                {
                    //Debug.Log("Doesn't seem like I can stand on this");
                    edge = transform.position;
                    climbablePoint = Vector3.zero;
                }
            }
        }
        else
        {//We are not close to an object
            //Reset reach height and climbable point
            reachHeight = 0;
            climbablePoint = Vector3.zero;
            climbableObject = null;
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

    public bool climbableDistance(Vector3 dist1, Vector3 dist2)
    {
        //Debug.Log("Distance: "+Vector3.Distance(dist1, dist2) +", Max Reach: "+maxReachDist);
        return (maxReachDist>Vector3.Distance(dist1, dist2));
    }
}
