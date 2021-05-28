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
    private ClimbChecker cCheck;
    private GameObject focus;//will be either thing to climb on or to pick up
    private RaycastHit mostRecentHit;
    private Vector3 goToPoint;//point which hands will center around
    public bool facingImportant = false;

    //public float handOffset = .3f,handRestingHeight = .5f; for later
    // Start is called before the first frame update
    void Start()
    {
        handMesh = GetComponent<MeshFilter>();
        handMesh.mesh = relaxedHand;

        hQ = (int)handedness;//makes hand direction simpler
        pAbil = FindObjectOfType<PlayerAbilityController>();
        cCheck = FindObjectOfType<ClimbChecker>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("check result: " + checkInFront());
        //Debug.Log("current hand mesh: "+handMesh.mesh);           
        if(cCheck.climbablePoint != Vector3.zero && handMesh.mesh!=climbHand)
        {
            //climbing hands
            handMesh.mesh = climbHand;
            Debug.Log("Climb Hands Selected");
        }
        else if(pAbil.holdingObject && handMesh.mesh != grabHand)
        {
            //grab hands
            handMesh.mesh = grabHand;
            Debug.Log("Grab Hands Selected");
        }
        else
        {
            //resting hands
            handMesh.mesh = relaxedHand;
            Debug.Log("Relaxed Hand Selected");
        }
        
        //transform.Rotate(Vector3.up * hQ, Space.Self);
    }
    
    //called by climb checker to inform hands whether there is an interesting object ahead
    private bool findSignificant(GameObject t)
    {
        Debug.Log("called findSignificant");
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

    //runs either to check if the set of hits brings with it an object that is important
    //or can be run just to clear facing important
    public bool checkInFront()
    {
        //Essentially all repurposed from climb checker, had to move it over due to timing issues. 
        //Vector3 landingRayStart = (cCheck.transform.position + cCheck.maxClimbHeight * Vector3.up);
        //Ray landingRay = new Ray(landingRayStart, transform.forward * cCheck.maxDepth);
        //Vector3 canStandRayStart = landingRayStart + (landingRay.direction * cCheck.maxDepth);
        Ray castDir = new Ray(transform.position, -transform.up);
        Debug.DrawRay(castDir.origin, castDir.direction, Color.grey);
        RaycastHit[] hits = Physics.SphereCastAll(castDir, 1);
       

        foreach(RaycastHit hit in hits)
        {
            if (findSignificant(hit.collider.gameObject))
            {
                return true;
            }
        }

        return false; 
    }

    

    private void LateUpdate()
    {
        
    }
}

/* private void updateHandLocation()
 {

     Vector3 goToPoint = (transform.position + (Vector3.up * handRestingHeight));

     if (edge != transform.position)
     {
         goToPoint = edge;
     }
     //Debug.Log("Go to point: "+goToPoint);

     //rhand.position = Vector3.Lerp(rhand.position, goToPoint + (transform.right * handOffset), handMoveSpeed);
     //if (!(pAbil.currentGrab != null && pAbil.heldObject && pAbil.heldObject.gameObject.GetComponent<SquashedObject>() != null))
     //{
         //lhand.position = Vector3.Lerp(lhand.position, goToPoint - (transform.right * handOffset), handMoveSpeed);
     //}
     if (Vector3.Distance(rhand.position, edge) < .1)
     {
         //hand closed indicator
     }
 }*/
