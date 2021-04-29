using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    public float checkStep = .1f, reachHeight = 0,
                 climbLengthDepth = 1.5f, maxClimbHeight = 2,
                 handOffset = .3f, handMoveSpeed = .3f,
                 detectRadius = 1f, landingDepth = .5f,
                 maxReachDist = 4f, reachDepth = 1f;
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

    public void clear()
    {
        reachHeight = 0;
        climbablePoint = Vector3.zero;
        climbableObject = null;
        if (!pMC.climbing)
        {
            edge = transform.position;
        }
    }

    //should be a coroutine for performance, but testing the idea first. 
    public void adjustHeight()
    {
        //Checks if player is close to object
        RaycastHit proxHit;
        Ray proxRay = new Ray(transform.position, transform.forward);
       
        Debug.DrawRay(transform.position, (transform.forward*reachDepth), Color.blue);
        //Debug.Log("Detecting object: " + Physics.SphereCast(proxRay, detectRadius, out proxHit, reachDepth));
        if (Physics.SphereCast(proxRay,detectRadius, out proxHit, reachDepth)&&(proxHit.collider.isTrigger == false))
        {
            //Object Gabe could concievably climb
            climbableObject = proxHit.transform.gameObject;

            RaycastHit canLandHit;
            //current "center" of character
            Vector3 hcPos = (transform.position+Vector3.down) + (transform.up * reachHeight);

            Ray landingRay = new Ray(hcPos, transform.forward * climbLengthDepth);
            Debug.DrawRay(landingRay.origin, landingRay.direction, Color.yellow);
            //Debug.Log(Physics.Raycast(landingRay, out canLandHit, climbLengthDepth));
            if (Physics.Raycast(landingRay, out canLandHit, climbLengthDepth)&& (canLandHit.collider.isTrigger == false))
            {
                
                if (reachHeight <= maxClimbHeight)
                {
                    //Debug.Log("Should be incrementing");
                    reachHeight += checkStep;//raises the reach ever so slightly
                }
            }
            //if our landing ray isn't hitting anything
            //Debug.Log((!Physics.Raycast(landingRay, out canLandHit, climbLengthDepth)) +" and "+ (reachHeight != 0));
            if ((!Physics.Raycast(landingRay, out canLandHit, climbLengthDepth)|| (canLandHit.collider.isTrigger == true)) && (reachHeight != 0))
            {//We are close enough and HAVE found the objects top 
            
                Vector3 possEdge = new Vector3(transform.position.x + landingRay.direction.x,
                                  (transform.position.y-1) + reachHeight,
                                  transform.position.z + landingRay.direction.z);

                //Can stand ray slightly upward to deal with the awkward shape of boxes
                //This ray checks to see if player were to stand on the final spot, that there would actually be ground there
                Ray canStandRay = new Ray((possEdge + (Vector3.up*.5f) + transform.forward*landingDepth), -transform.up);
                Debug.DrawRay(canStandRay.origin, canStandRay.direction, Color.green);

                RaycastHit canStandHit;
                //Debug.Log((!Physics.Raycast(landingRay, out canLandHit, climbLengthDepth)) + " and " + (reachHeight != 0));
                if (Physics.Raycast(canStandRay, out canStandHit, 2f) && (canStandHit.collider.isTrigger == false))
                {
                    //Debug.Log("Can stand on: " + canStandHit.collider.name);
                    edge = possEdge;
                    climbablePoint = new Vector3(edge.x, edge.y + (playerHeight * 1.3f), edge.z) + transform.forward * landingDepth;
                }
            }
        }
        else
        {//We are not close to an object
            //Reset reach height and climbable point
            clear();
        }
    }


    private void updateHands()
    {
        {
            rhand.position = Vector3.Lerp(rhand.position, edge + (transform.right * handOffset), handMoveSpeed);
            if (!(pAbil.currentGrab != null && pAbil.heldObject && pAbil.heldObject.gameObject.GetComponent<SquashedObject>() != null)){
                lhand.position = Vector3.Lerp(lhand.position, edge - (transform.right * handOffset), handMoveSpeed);
            }
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

