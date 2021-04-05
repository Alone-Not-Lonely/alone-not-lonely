using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChestDoorCheck : MonoBehaviour
{
    //public int weightCounter = 0, openTime = 100;
    public float rayLength =1f, offsetLength = 1.75f, offsetHeight = .6f;
    private Ray leftRay, rightRay;
    private BoxContactBehavior _behavior;
    private AtticLadderController _ladder;
    // Start is called before the first frame update
    void Awake()
    {
        _behavior = GetComponent<BoxContactBehavior>();
        _ladder = FindObjectOfType<AtticLadderController>();
    }

    void LateUpdate()
    {
        
        if (onDoor())
        {
            _ladder.open();
        }
        else
        {
            _ladder.close();
        }

        //if (weightCounter > openTime)
        //{
            //_ladder.AnimateOpenLadder();
            //_ladder.EnableDisableLadders();
        //}

    }

    bool onDoor()
    {
        if (_behavior.beingHeld)
        {
            return false;
        }

        leftRay = new Ray((transform.position-transform.right* offsetLength - transform.up * offsetHeight), -transform.up * rayLength);
        Debug.DrawRay(leftRay.origin, leftRay.direction*rayLength, Color.red);
        RaycastHit leftOut;

        if(!Physics.Raycast(leftRay, out leftOut, rayLength))
        {
            return false;
        }
        
        if(leftOut.collider.tag != "Finish")
        {
            Debug.Log(leftOut.collider.tag);
            return false;
        }

        rightRay = new Ray((transform.position + transform.right * offsetLength - transform.up*offsetHeight), -transform.up * rayLength);
        Debug.DrawRay(rightRay.origin, rightRay.direction * rayLength, Color.red);
        RaycastHit rightOut;

        if (!Physics.Raycast(rightRay, out rightOut, rayLength))
        {
            return false;
        }

        if (rightOut.collider.tag != "Finish")
        {
            return false;
        }

        return true;
    }
}
