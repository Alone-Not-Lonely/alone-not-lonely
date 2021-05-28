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
    private GameObject focus;
    private RaycastHit mostRecentHit;
    //public float handOffset = .3f; for later
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
    void Update()
    {
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

    public void recieveNearHit(RaycastHit hit)
    {
        //Debug.Log("Recieving hit");
        focus = hit.transform.gameObject;
        mostRecentHit = hit;
    }

    private void LateUpdate()
    {
        
    }
}

/* private void updateHands()
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
