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
    private bool waiting;
    public float waitTime = 2f;
    private float currentWaitTime;
    void Start()
    {
        targetPoint = this.transform.position + (this.transform.up * height);
        //Debug.Log(this.gameObject.name);
       //Debug.Log(this.transform.up * height);
        //Debug.Log(this.transform.position);
        //Debug.Log(targetPoint);
        holdingObject = false;
        goingUp = true;
        waiting = false;
        currentWaitTime = 0;
    }

    void FixedUpdate()
    {
        if(holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) > .1f)
        {
            heldObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(heldObject.transform.position, targetPoint, Time.fixedDeltaTime * speed));
        }
        
        if (!waiting && goingUp && holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) <= .1f)
        {
            targetPoint = startPoint;
            goingUp = !goingUp;
            waiting = true;
        }
        else if(!waiting && !goingUp && holdingObject && Vector3.Distance(heldObject.transform.position, targetPoint) <= .1f)
        {
            targetPoint = this.transform.position + (this.transform.up * height);
            goingUp = !goingUp;
            waiting = true;
        }
        else if(waiting && currentWaitTime < waitTime)
        {
            currentWaitTime += Time.deltaTime;
        }
        else if(waiting && currentWaitTime >= waitTime)
        {
            waiting = false;
            currentWaitTime = 0;
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

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Grabable") && !other.gameObject.GetComponent<BoxContactBehavior>().beingHeld && !this.holdingObject && !other.isTrigger)
        {
            GrabAttempt(other.gameObject, this.gameObject);
            if(this.holdingObject)
            {
                startPoint = this.transform.position - (transform.localScale.y * transform.up) + new Vector3(0, other.gameObject.transform.localScale.y/2, 0);
            }
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Grabable") && !other.gameObject.GetComponent<BoxContactBehavior>().beingHeld && !this.holdingObject && !other.isTrigger)
        {
            GrabAttempt(other.gameObject, this.gameObject);
        }
    }
}
