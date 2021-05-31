using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChestDoorCheck : MonoBehaviour
{
    //public int weightCounter = 0, openTime = 100;
    public float rayLength = 1f, offsetLength = 1.75f, offsetHeight = .6f;
    private Ray leftRay, rightRay;
    private BoxContactBehavior _behavior;
    private AtticLadderController _ladder;
    [SerializeField]
    private ContextualUI cu;

    bool wasOnDoor = false;

    public AudioSource atticCreak;


    void Awake()
    {
        _behavior = GetComponent<BoxContactBehavior>();
        _ladder = FindObjectOfType<AtticLadderController>();
        //atticCreak.Play();
    }

    void LateUpdate()
    {

        if (onDoor())
        {
            if (!wasOnDoor)
            {
                _ladder.gameObject.GetComponent<AudioSource>().Play();
                Debug.Log("playing");
                atticCreak.Play();
            }
            _ladder.open();
            wasOnDoor = true;
            //Pushes prompt along only once
            if (cu.getCurrInd() == 0)
            {
                cu.nextPrompt();
            }

        }
        else
        {
            if(wasOnDoor)
            {
                Debug.Log("Stopping");
                atticCreak.Stop();
            }
            wasOnDoor = false;
            _ladder.close();
        }
    }

    bool onDoor()
    {
        if (_behavior.beingHeld)
        {
            return false;
        }

        leftRay = new Ray((transform.position - transform.right * offsetLength - transform.up * offsetHeight), -transform.up * rayLength);
        Debug.DrawRay(leftRay.origin, leftRay.direction * rayLength, Color.red);
        RaycastHit leftOut;

        if (!Physics.Raycast(leftRay, out leftOut, rayLength))
        {
            return false;
        }

        if (leftOut.collider.tag != "Finish")
        {
            return false;
        }

        rightRay = new Ray((transform.position + transform.right * offsetLength - transform.up * offsetHeight), -transform.up * rayLength);
        Debug.DrawRay(rightRay.origin, rightRay.direction * rayLength, Color.red);
        RaycastHit rightOut;

        if (!Physics.Raycast(rightRay, out rightOut, rayLength, ~LayerMask.GetMask("Ignore Raycast")))  
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