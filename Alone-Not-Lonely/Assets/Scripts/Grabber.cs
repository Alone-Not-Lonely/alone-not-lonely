using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabber : MonoBehaviour
{
    public GameObject heldObject;
    public bool holdingObject;
    public float holdDistance;
    private void Start() 
    {
        heldObject = null;
        holdingObject = false;    
    }
    public void GrabAttempt(GameObject objectInRange, GameObject holder)
    {
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
            objectInRange.GetComponent<BoxContactBehavior>().boxHolder.GetComponent<Grabber>().ReleaseObject();
            heldObject = objectInRange;
            heldObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            holdingObject = true;
            heldObject.GetComponent<BoxContactBehavior>().beingHeld = true;
            heldObject.GetComponent<BoxContactBehavior>().boxHolder = holder;
        }
    }

    public void ReleaseObject()
    {
        if(heldObject && holdingObject)
        {
            heldObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            heldObject.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            heldObject.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            holdingObject = false;
            heldObject.GetComponent<BoxContactBehavior>().beingHeld = false;
            heldObject.GetComponent<BoxContactBehavior>().boxHolder = null;
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

}
