using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabber : MonoBehaviour
{
    
    public GameObject heldObject;
    public bool holdingObject;
    public float holdDistance;
    public float grabCooldown = 2f;
    public float currentGrabCooldown;
    public bool inGrabCooldown;
    

    private void Start() 
    {
        heldObject = null;
        holdingObject = false;    
    }

    protected void GrabAttempt(GameObject objectInRange, GameObject holder)
    {
        Debug.Log(gameObject.name + " is Calling");
        if(!inGrabCooldown)// && holder == this.gameObject)
        {
            Debug.Log("Can't Grab Yet");
            if(!objectInRange.GetComponent<BoxContactBehavior>().beingHeld)
            {
                heldObject = objectInRange;
                heldObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                holdingObject = true;
                heldObject.GetComponent<BoxContactBehavior>().beingHeld = true;
                heldObject.GetComponent<BoxContactBehavior>().boxHolder = holder;
            }
            else
            {
                Debug.Log("Grabbed");
                objectInRange.GetComponent<BoxContactBehavior>().boxHolder.GetComponent<Grabber>().ReleaseObject();
                objectInRange.GetComponent<BoxContactBehavior>().beingHeld = false;
                heldObject = objectInRange;
                heldObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                holdingObject = true;
                heldObject.GetComponent<BoxContactBehavior>().beingHeld = true;
                heldObject.GetComponent<BoxContactBehavior>().boxHolder = holder;
            }
        }

    }

    public void ReleaseObject()
    {
        if(heldObject && holdingObject)
        {
            Debug.Log("Released");
            heldObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            heldObject.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            heldObject.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            holdingObject = false;
            heldObject.GetComponent<BoxContactBehavior>().beingHeld = false;
            heldObject.GetComponent<BoxContactBehavior>().boxHolder = null;
            heldObject = null;
            inGrabCooldown = true;
        }
    }
    public void FixedUpdate(Transform holderObject, Vector3 holdOffsetDir)
    {
        if(heldObject != null && holdingObject)
        {
            heldObject.GetComponent<Rigidbody>().MovePosition(holderObject.position + (- holderObject.up * .65f) + holdOffsetDir * holdDistance);
            heldObject.GetComponent<Rigidbody>().MoveRotation(holderObject.rotation);
        }
    }

    public void Update()
    {
        if(inGrabCooldown && currentGrabCooldown < grabCooldown)
        {
            currentGrabCooldown+=Time.deltaTime;
        }
        else if(inGrabCooldown && currentGrabCooldown >= grabCooldown)
        {
            currentGrabCooldown = 0;
            inGrabCooldown = false;
        }
    }

}
