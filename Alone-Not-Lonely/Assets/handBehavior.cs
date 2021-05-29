using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handBehavior : MonoBehaviour
{
    protected MeshFilter handMesh;

    public Mesh relaxedHand;
    public Mesh grabHand;
    public Mesh climbHand;

    private enum handed {right = 1, left = -1};
    [SerializeField]
    private handed handedness;
    private int hQ;
    private PlayerAbilityController pAbil;
    private PlayerMovementController pMov;
    private ClimbChecker cCheck;
    private GameObject focus;//will be either thing to climb on or to pick up
    private RaycastHit mostRecentHit;
    private Vector3 goToPoint;//point which hands will center around
    public bool facingImportant = false;
    //private List<GameObject> nearSig;//contains nearby objects of interest

    public float handOffset = .3f,handRestingHeight = .5f, forwardAmount = .5f,
                 handMoveSpeed = .3f;
    // Start is called before the first frame update
    void Start()
    {
        handMesh = GetComponent<MeshFilter>();
        handMesh.mesh = relaxedHand;
        goToPoint = transform.position;//lazy way of instantiation
        hQ = (int)handedness;//makes hand direction simpler
        pAbil = FindObjectOfType<PlayerAbilityController>();
        cCheck = FindObjectOfType<ClimbChecker>();
        pMov = FindObjectOfType<PlayerMovementController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!pMov.climbing)//maintains consistant gotopoint in case of climbing
        {
            goToPoint = (cCheck.transform.position + (Vector3.up * handRestingHeight) + transform.right * hQ * handOffset);
        }
        

        /*Priority order:
         * 1) Mid Climb must be Climb Hands
         * 2) Holding object must be holding hands
         * 3) If in front of an object of importance, reach out to it, relaxed hands
         * 4) If ONLY facing a climbable object, put climbing hands on edge
         */

        if (pMov.climbing)
        {
            if (handMesh.mesh != climbHand)
            {
                handMesh.mesh = climbHand;
            }
            //we actively avoid updating goToPoint
        }
        else if (pAbil.holdingObject && handMesh.mesh != grabHand)
        {
            handMesh.mesh = grabHand;
            BoxCollider temp = getObjectsCollider(pAbil.currentGrab);
            //pray temp doesn't return null for now
            goToPoint = getSideHit(temp);
        }
        else if (facingImportant)
        {
            //put hands out forward
            goToPoint += forwardAmount * cCheck.transform.forward;
            if (handMesh.mesh != relaxedHand)
            {
                handMesh.mesh = relaxedHand;
            }
        }
        else if (cCheck.climbablePoint != Vector3.zero && handMesh.mesh != climbHand)
        {
            handMesh.mesh = climbHand;

            goToPoint = cCheck.edge + (transform.right * hQ * handOffset);
        }
        else if (handMesh.mesh != relaxedHand)
        {
            //resting hands
            handMesh.mesh = relaxedHand;
            //Debug.Log("Relaxed Hand Selected");
        }
        if (pAbil.holdingObject)
        {
            transform.position = goToPoint;//Snap to sides of object if carrying
        }
        transform.position = Vector3.Lerp(transform.position, goToPoint, handMoveSpeed);
        //transform.Rotate(Vector3.up * hQ, Space.Self);
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
                firePoint = hit.point + transform.forward * .2f + transform.right * hQ * 5f;//creates a point slightly back from contact point and sufficiently away
            }
        }
      
        Ray fireBack = new Ray(firePoint, transform.right * -1 * hQ);//fires ray back at object
        RaycastHit[] fireBackHits = Physics.RaycastAll(fireBack);

        foreach(RaycastHit hit in fireBackHits)
        {

            if (hit.collider == currGrab)
            {
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

        //Debug.Log("Searching in object: " + t.name);
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
    

    private void LateUpdate()
    {
        
    }
}
