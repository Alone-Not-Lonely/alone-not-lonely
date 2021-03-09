using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    public float checkStep = .1f, reachHeight = 0, climbLengthDepth = 1.5f, maxClimbHeight = 2;
    private float reachDepth = 1f;
    private float playerHeight;
    public Vector3 climbablePoint = Vector3.zero;

    private Transform pTransform;
    private CharacterController pControl;
    private PlayerAbilityController pAbil;

    //Scripts using this will check if Vector3.zero
    //So lets just be sure not to have any climbable places at origin

    // Start is called before the first frame update
    void Start()
    {
        pAbil = GetComponentInParent<PlayerAbilityController>();
        pControl = GetComponentInParent<CharacterController>();
        pTransform = GetComponentInParent<Transform>();
        playerHeight = GetComponentInParent<CharacterController>().height;
    }

    private void FixedUpdate()
    {
        if (pControl.isGrounded && !pAbil.holdingObject) { adjustHeight(); }
    }

    //should be a coroutine for performance, but testing the idea first. 
    public void adjustHeight()
    {
        RaycastHit proxHit;
        Ray proxRay = new Ray(transform.position, transform.forward);
       
        Debug.DrawRay(transform.position, (transform.forward*reachDepth), Color.blue);
        if (Physics.Raycast(proxRay, out proxHit, reachDepth))
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
                climbablePoint = new Vector3(transform.position.x + landingRay.direction.x,
                                             transform.position.y + (playerHeight * 1.3f),
                                             transform.position.z + landingRay.direction.z);
            }
            
        }
        else
        {//We are not close to an object
            //Reset reach height and climbable point
            reachHeight = 0;
            climbablePoint = Vector3.zero;
        }
    }

    private float posDiff()
    {
        return (transform.position.y - pTransform.position.y);
    }
}
