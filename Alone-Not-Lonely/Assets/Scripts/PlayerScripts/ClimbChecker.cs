using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    [SerializeField]
    public float checkStep = .1f, reachHeight = 0;
    public float reachLength = .5f, climbCheckLength = 1;
    private Transform pTransform;
    public bool canClimb = false;
    public Vector3 climbablePoint = Vector3.zero;
    //Scripts using this will check if Vector3.zero
    //So lets just be sure not to have any climbable places at origin
   
    // Start is called before the first frame update
    void Start()
    {
        pTransform = GetComponentInParent<Transform>();
    }

    private void FixedUpdate()
    {
        
        adjustHeight();
    }

    //should be a coroutine for performance, but testing the idea first. 
    public void adjustHeight()
    {
        RaycastHit proxHit;
        Ray proxRay = new Ray(transform.position, transform.forward);
       
        

        //Debug.Log(transform.forward * reachLength);
        Debug.DrawRay(transform.position, (transform.forward*reachLength), Color.blue);
        if (Physics.Raycast(proxRay, out proxHit, reachLength))
        {
            RaycastHit canLandHit;
            Vector3 hcPos = transform.position + (transform.up * reachHeight);
          
            Ray canLandRay = new Ray(hcPos, transform.forward);
            Debug.DrawRay(canLandRay.origin, canLandRay.direction, Color.yellow);
            if (Physics.Raycast(canLandRay, out canLandHit, climbCheckLength))
            {//We are close to an object and have NOT yet found its top

                if (reachHeight < 1) { reachHeight += checkStep; }//raises the reach ever so slightly
            }
            else
            {//We are close enough and HAVE found the objects top
                climbablePoint = new Vector3(transform.position.x, transform.position.y + reachHeight, transform.position.z + climbCheckLength);
            }
        }
        else
        {
            //Reset reach height
            reachHeight = 0;
            //if (canClimb)
            //{
            //    canClimb = false;
            //}
        }


        //move to 1 height, stopping if at any point we leave the object
        //if (posDiff() <= 1 && inObject)
        //{
        //    transform.position = transform.position + Vector3.up * checkStep;
        //}
    }

    private float posDiff()
    {
        return (transform.position.y - pTransform.position.y);
    }
}
