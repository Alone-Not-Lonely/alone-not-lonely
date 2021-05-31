using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handBehavior : MonoBehaviour
{
    protected MeshFilter handMesh;

    public Mesh relaxedHand;
    public Mesh grabHand;
    public Mesh climbHand;
    public float restingYRot, holdingYRot;
    public Vector3 climbingRotations;
    private enum handed { right = 1, left = -1 };
    private enum modes { grabbing, relaxed, climbing}
    private modes myMode = modes.relaxed;
    [SerializeField]
    private handed handedness;
    private int hQ;
    private PlayerAbilityController pAbil;
    private PlayerMovementController pMov;
    private ClimbChecker cCheck;
    private Vector3 goToPoint, prevSideResult, holdingOffset;//point which hands will center around
    public bool facingImportant = false;
    private float holdingXOffset;
    //private Transform origParent;
    

    public float handOffset = .3f,handRestingHeight = .5f, forwardAmount = .5f,
                 handMoveSpeed = .3f;
    // Start is called before the first frame update
    void Start()
    {
        //origParent = transform.parent;
        handMesh = GetComponent<MeshFilter>();
        handMesh.mesh = relaxedHand;
        prevSideResult = Vector3.zero;
        //transform.localEulerAngles = new Vector3(0, restingYRot, 0);
        transform.localRotation = Quaternion.Euler(0, restingYRot, 0);
        //transform.eulerAngles = new Vector3(0, restingYRot, 0);
        goToPoint = transform.position + ((Vector3.up * handRestingHeight) + transform.right * hQ * handOffset);//lazy way of instantiation
        hQ = (int)handedness;//makes hand direction simpler
        pAbil = FindObjectOfType<PlayerAbilityController>();
        cCheck = FindObjectOfType<ClimbChecker>();
        pMov = FindObjectOfType<PlayerMovementController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!pMov.climbing && !pAbil.holdingObject)//maintains consistant gotopoint in case of climbing
        {
            goToPoint = (cCheck.transform.position + (Vector3.up * handRestingHeight) + transform.right * hQ * handOffset);
        }

        if (!pAbil.holdingObject)
        {
            prevSideResult = Vector3.zero;
        }

        /*Priority order:
         * 1) Mid Climb must be Climb Hands
         * 2) Holding object must be holding hands
         * 3) If in front of an object of importance, reach out to it, relaxed hands
         * 4) If ONLY facing a climbable object, put climbing hands on edge
         */

        

        if (pMov.climbing)
        {
            if (myMode != modes.climbing)
            {
                handMesh.mesh = climbHand;
                myMode = modes.climbing;
                transform.localRotation = Quaternion.Euler(climbingRotations);
            }
            goToPoint = cCheck.edge + (cCheck.transform.right * hQ * -1 * handOffset) + cCheck.transform.forward * .5f;
            //we actively avoid updating goToPoint
        }
        else if (pAbil.holdingObject)
        {
            //sets location as few times as possible
            BoxCollider temp = getObjectsCollider(pAbil.currentGrab);
            
            if (prevSideResult == Vector3.zero && temp!=null)
            {
                
                prevSideResult = getSideHit(temp);

                holdingXOffset = Vector3.Magnitude(temp.transform.position - prevSideResult) * -hQ; //Technically a little long but works out to be good enough
            }
            if (prevSideResult != Vector3.zero && temp!= null)
            {
                goToPoint = temp.transform.position + cCheck.transform.right * holdingXOffset - cCheck.transform.forward*.8f;
            }
            
            if (myMode != modes.grabbing)
            {
                myMode = modes.grabbing;
                handMesh.mesh = grabHand;
                //pray temp doesn't return null for now
                transform.localRotation = Quaternion.Euler(0, holdingYRot, 0);
            }
            //goToPoint = prevSideResult;
        }
        else if (facingImportant)
        {
            //put hands out forward
            goToPoint += forwardAmount * cCheck.transform.forward;
            
            if (myMode != modes.relaxed)
            {
                myMode = modes.relaxed;
                transform.localRotation = Quaternion.Euler(0, restingYRot, 0);
                handMesh.mesh = relaxedHand;
            }
        }
        else if (cCheck.climbablePoint != Vector3.zero)
        {
           
            if (myMode != modes.climbing) {

                myMode = modes.climbing;
                handMesh.mesh = climbHand;
            }
            goToPoint = cCheck.edge + (cCheck.transform.right * hQ * -1 * handOffset) + cCheck.transform.forward*.5f;
            transform.localRotation = Quaternion.Euler(climbingRotations);
        }
        else 
        {
            if (myMode!=modes.relaxed) {
                handMesh.mesh = relaxedHand;
                myMode = modes.relaxed;
                transform.localRotation = Quaternion.Euler(0, restingYRot, 0);
            }
        }

        if (pAbil.holdingObject)
        {
            transform.position = goToPoint;//Snap to sides of object if carrying
        }

        transform.position = Vector3.Lerp(transform.position, goToPoint, handMoveSpeed);
    }

    public void handCast(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            if (findSignificant(hit.collider))
            {
                facingImportant = true;
                return;
            }
        }
        facingImportant = false;
        return;
    }

    private BoxCollider getObjectsCollider(GameObject obj)
    {
        
        foreach(BoxCollider col in obj.GetComponentsInChildren<BoxCollider>())
        {
            if (!col.isTrigger)
            {
                return col;
            }
        }
        return null;
    }
    
    //Takes an object and returns first collider that is NOT a trigger
    private Vector3 getSideHit(BoxCollider currGrab)
    {
        Vector3 firePoint = Vector3.zero;
        RaycastHit[] initHits = Physics.RaycastAll(cCheck.transform.position, (currGrab.transform.position - cCheck.transform.position));//fires ray directly forward
        foreach(RaycastHit hit in initHits)
        {
            if (hit.collider == currGrab)
            {
                firePoint = hit.point + hit.transform.forward * .1f + hit.transform.right * -1 *hQ * 5f;//creates a point slightly back from contact point and sufficiently away
            }
        }
        Ray fireBack = new Ray(firePoint, cCheck.transform.right  * hQ);//fires ray back at object
        RaycastHit[] fireBackHits = Physics.RaycastAll(fireBack);
        Debug.DrawRay(fireBack.origin, fireBack.direction, Color.cyan);
        foreach(RaycastHit hit in fireBackHits)
        {
            if (hit.collider == currGrab)
            {
                Debug.Log(hit.point);
                return hit.point;//creates a point slightly back from contact point and sufficiently away

            }
        }
        return Vector3.zero;
    }
    
    
    //called by climb checker to inform hands whether there is an interesting object ahead
    private bool findSignificant(Collider c)
    {
        GameObject t = c.gameObject;

        if(t.tag == "Player")
        {
            return false;
        }
        
        //check objects themselves
        if (t.GetComponent<BoxContactBehavior>()!=null || t.GetComponent<KeyBaring>() != null)
        {
            return true;
        }

        //check if they have children with the components
        if(t.GetComponentInChildren<BoxContactBehavior>() != null || t.GetComponentInChildren<KeyBaring>() != null)
        {
            return true;
        }
        //probably going to have to deal with the parent problem but will hold off for now
    
        return false;
    }
}
