using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChestDoorCheck : MonoBehaviour
{
    public int weightTimer = 0, openTime = 100;
    private float rayLength =1f, offsetLength = 1.75f, offsetHeight = .6f;
    private Ray leftRay, rightRay;
    private BoxContactBehavior _behavior;
    private AtticLadderController _ladder;
    // Start is called before the first frame update
    void Awake()
    {
        _behavior = GetComponent<BoxContactBehavior>();
        _ladder = FindObjectOfType<AtticLadderController>();
    }

    void FixedUpdate()
    {

        if (onDoor())
        {
            weightTimer++;
        }
        else
        {
            if (weightTimer > 0) { weightTimer--; }
        }

        if (weightTimer > openTime)
        {
            _ladder.AnimateOpenLadder();
            _ladder.EnableDisableLadders();
        }

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
            Debug.Log("Left Not touching anything!");
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
            Debug.Log("Right Not touching anything!");
            return false;
        }

        if (rightOut.collider.tag != "Finish")
        {
            Debug.Log("Object aint finish!");
            return false;
        }

        return true;
    }
}
