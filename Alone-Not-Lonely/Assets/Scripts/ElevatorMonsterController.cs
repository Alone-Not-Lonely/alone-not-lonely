using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMonsterController : Grabber
{
    [Header("Elevator Monster")]
    //public GameObject heldObject;
    public Vector3 targetPoint;
    public float height = 2f;
    public float speed = 1f;

    //public bool holdingObject;
    private bool goingUp;
    void Start()
    {
        targetPoint = this.transform.position + (this.transform.up * height);
        holdingObject = false;
        goingUp = true;
    }

    void FixedUpdate()
    {
        if(holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) > .1f)
        {
            heldObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(heldObject.transform.position, targetPoint, .01f * speed));
            //heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, topPoint, .01f * speed);
        }
        
        if (goingUp && holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) <= .1f)
        {
            targetPoint = this.transform.position - this.transform.up;
            goingUp = !goingUp;
        }
        else if(!goingUp && holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) <= .1f)
        {
            targetPoint = this.transform.position + (this.transform.up * height);
            goingUp = !goingUp;
        }

    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable"))// && !other.gameObject.GetComponent<BoxContactBehavior>().beingHeld)
        {
            GrabAttempt(other.gameObject, this.gameObject);
        }
    }

    void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable"))// && !other.gameObject.GetComponent<BoxContactBehavior>().beingHeld)
        {
            GrabAttempt(other.gameObject, this.gameObject);
        }
    }
}
