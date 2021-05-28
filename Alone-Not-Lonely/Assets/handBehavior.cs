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
        Debug.Log("current hand mesh: "+handMesh.mesh);           
        if(cCheck.climbablePoint != Vector3.zero && handMesh.mesh!=climbHand)
        {
            //climbing hands
            //handMesh.mesh = climbHand;
        }
        else if(pAbil.holdingObject && handMesh.mesh != grabHand)
        {
            //grab hands
            //handMesh.mesh = grabHand;
        }
        else
        {
            //resting hands
            //handMesh.mesh = relaxedHand;
        }
        
        //transform.Rotate(Vector3.up * hQ, Space.Self);
    }
}
