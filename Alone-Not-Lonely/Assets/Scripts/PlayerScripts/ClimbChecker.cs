using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public float reachHeight = 0,
                 maxDepth = 1.5f, maxClimbHeight = 2,
                 handOffset = .3f, handMoveSpeed = .3f,
                 detectRadius = 1f, landingDepth = .5f,
                 maxReachDist = 4f, reachDepth = 1f,
                 handRestingHeight = .5f;
    private float playerHeight, playerRadius = 1f;
    public Vector3 climbablePoint = Vector3.zero;
    private Vector3 edge;
    public Transform lhand, rhand;
    private Transform pTransform;
    private CharacterController pControl;
    private PlayerAbilityController pAbil;
    private PlayerMovementController pMC;
    public GameObject climbableObject;

    //Dealing with hand model swaps
    protected MeshFilter rHandMesh;
    protected MeshFilter lhandMesh;

    public Mesh relaxedRHand;
    public Mesh relaxedLHand;
    public Mesh grabRHand;
    public Mesh grabLHand;
    public Mesh climbRHand;
    public Mesh climbLHand;

    //Scripts using this will check if Vector3.zero
    //So lets just be sure not to have any climbable places at origin

    // Start is called before the first frame update
    void Start()
    {
        ignoreLayer = LayerMask.GetMask("ElevatorMonster");
        edge = transform.position;
        pAbil = GetComponentInParent<PlayerAbilityController>();
        pControl = GetComponentInParent<CharacterController>();
        pTransform = GetComponentInParent<Transform>();
        playerHeight = GetComponentInParent<CharacterController>().height;
        playerRadius = GetComponentInParent<CharacterController>().radius;
        pMC = GetComponentInParent<PlayerMovementController>();
        rHandMesh = rhand.GetComponentInChildren<MeshFilter>();
        lhandMesh = lhand.GetComponentInChildren<MeshFilter>();
        clear();
    }

    private void FixedUpdate()
    {
        if (pControl.isGrounded) { adjustHeight(); }
        updateHands();
        if(climbablePoint != Vector3.zero)
        {
            //climbing hands
            rHandMesh.mesh = climbRHand;
            lhandMesh.mesh = climbLHand;
        }
        else if(pAbil.holdingObject)
        {
            //grab hands
            rHandMesh.mesh = grabRHand;
            lhandMesh.mesh = grabLHand;
        }
        else
        {
            //resting hands
            rHandMesh.mesh = relaxedRHand;
            lhandMesh.mesh = relaxedLHand;
        }
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
        RaycastHit obNearHit;
        Ray obNearRay = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, (transform.forward * reachDepth), Color.blue);
        //If an object is close enough to gabe to climb:
        if (Physics.SphereCast(obNearRay,detectRadius, out obNearHit, reachDepth, ~ignoreLayer) && (obNearHit.collider.isTrigger == false))
        {
            //Debug.Log("Hit something");
            //Object Gabe could concievably climb

            RaycastHit canLandHit;
            Vector3 landingRayStart = (transform.position + maxClimbHeight * Vector3.up);
            Ray landingRay = new Ray(landingRayStart, transform.forward * maxDepth);
            Debug.DrawRay(landingRay.origin, landingRay.direction, Color.yellow);

            //If the landing is short enough for gabe to climb:
            if (!Physics.Raycast(landingRay, out canLandHit, maxDepth, ~ignoreLayer) || (canLandHit.collider.isTrigger == true))
            {//We are close enough and HAVE found the object's top
                Vector3 canStandRayStart = landingRayStart + (landingRay.direction * maxDepth);
                Ray canStandRay = new Ray(canStandRayStart, -transform.up);
                Debug.DrawRay(canStandRay.origin, canStandRay.direction, Color.green);

                RaycastHit canStandHit;
                //Debug.Log((!Physics.Raycast(landingRay, out canLandHit, maxDepth)) + " and " + (reachHeight != 0));
                if (Physics.Raycast(canStandRay, out canStandHit, Mathf.Infinity, ~ignoreLayer) && (canStandHit.collider.isTrigger == false))
                {
                    //Debug.Log("Can stand on: " + canStandHit.collider.name);
                    //Debug.Log("Triggered by contact with: " + obNearHit.collider.name);
                    Vector3 possEdge = new Vector3(obNearHit.point.x, canStandHit.point.y, obNearHit.point.z);
                    //1.1 used to be 1.3
                    Vector3 probClimbPoint = new Vector3(canStandHit.point.x, edge.y + (playerHeight * 1.1f), canStandHit.point.z);
                    //Checks to see if the intended landing point is higher than the origin of the object that gabe first ran up against
                    //Possible flaws: If origin is somehow above the object (unlikely but possible)
                    if (possEdge.y > obNearHit.point.y)
                    {
                        //Debug.Log("Distances: edge y " + possEdge.y + ", ob y " + obNearHit.collider.transform.position.y);
                        climbableObject = canStandHit.transform.gameObject;
                        edge = possEdge;
                        climbablePoint = probClimbPoint;
                    }
                    else
                    {
                        clear();
                    }
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
        /*
        Vector3 goToPoint = (transform.position + (Vector3.up * handRestingHeight));

        if (edge != transform.position)
        {
            goToPoint = edge;
        }
        */
        //Debug.Log("Go to point: "+goToPoint);

        //rhand.position = Vector3.Lerp(rhand.position, goToPoint + (transform.right * handOffset), handMoveSpeed);
        //if (!(pAbil.currentGrab != null && pAbil.heldObject && pAbil.heldObject.gameObject.GetComponent<SquashedObject>() != null))
        //{
            //lhand.position = Vector3.Lerp(lhand.position, goToPoint - (transform.right * handOffset), handMoveSpeed);
        //}
        /*
        if (Vector3.Distance(rhand.position, edge) < .1)
        {
            //hand closed indicator
        }
        */
    }

    private float posDiff()
    {
        return (transform.position.y - pTransform.position.y);
    }

    public bool climbableDistance(Vector3 dist1, Vector3 dist2)
    {
        //Debug.Log("Distance: "+Vector3.Distance(dist1, dist2) +", Max Reach: "+maxReachDist);
        return (maxReachDist > Vector3.Distance(dist1, dist2));
    }
}
