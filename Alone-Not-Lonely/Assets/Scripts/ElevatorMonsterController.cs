using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMonsterController : Grabber
{
    [Header("Elevator Monster")]
    //public GameObject heldObject;
    public Vector3 targetPoint;
    public Vector3 startPoint;
    public float height = 2f;
    public float speed = 1f;

    //public bool holdingObject;
    private bool goingUp;
    void Start()
    {
        targetPoint = this.transform.position + (this.transform.up * height);
        Debug.Log(this.gameObject.name);
        Debug.Log(this.transform.up * height);
        Debug.Log(this.transform.position);
        Debug.Log(targetPoint);
        holdingObject = false;
        goingUp = true;
    }

    void FixedUpdate()
    {
        if(holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) > .1f)
        {
            Debug.Log(Vector3.MoveTowards(heldObject.transform.position, targetPoint, 1));
            heldObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(heldObject.transform.position, targetPoint, Time.fixedDeltaTime * speed));
        }
        
        if (goingUp && holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) <= .1f)
        {
            targetPoint = startPoint;
            goingUp = !goingUp;
        }
        else if(!goingUp && holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) <= .1f)
        {
            targetPoint = this.transform.position + (this.transform.up * height);
            goingUp = !goingUp;
        }

    }

    void HitTop()
    {
        if (goingUp && holdingObject)
        {
            targetPoint = startPoint;
            goingUp = !goingUp;
        }
    }

    void HitBottom()
    {
        if(!goingUp && holdingObject)
        {
            targetPoint = this.transform.position + (this.transform.up * height);
            goingUp = !goingUp;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable") && !other.gameObject.GetComponent<BoxContactBehavior>().beingHeld && !this.holdingObject)
        {
            GrabAttempt(other.gameObject, this.gameObject);
            if(this.holdingObject)
            {
                startPoint = other.gameObject.transform.position;
            }
        }
    }

    void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable") && !other.gameObject.GetComponent<BoxContactBehavior>().beingHeld && !this.holdingObject)
        {
            GrabAttempt(other.gameObject, this.gameObject);
        }
    }
}
