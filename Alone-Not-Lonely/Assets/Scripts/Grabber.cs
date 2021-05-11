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

    public bool GrabAttempt(GameObject objectInRange, GameObject holder)
    {
        //Debug.Log(gameObject.name + " is Calling");
        if(!inGrabCooldown)
        {
            if(!objectInRange.GetComponent<BoxContactBehavior>().beingHeld)
            {
                heldObject = objectInRange;
                heldObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                holdingObject = true;
                heldObject.GetComponent<BoxContactBehavior>().beingHeld = true;
                heldObject.GetComponent<BoxContactBehavior>().boxHolder = holder;
                heldObject.GetComponent<BoxContactBehavior>().boxSFX.PlayOneShot(heldObject.GetComponent<BoxContactBehavior>().boxPickup);
                return true;
            }
            else
            {
                objectInRange.GetComponent<BoxContactBehavior>().boxHolder.GetComponent<Grabber>().ReleaseObject();
                objectInRange.GetComponent<BoxContactBehavior>().beingHeld = false;
                heldObject = objectInRange;
                heldObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                holdingObject = true;
                heldObject.GetComponent<BoxContactBehavior>().beingHeld = true;
                heldObject.GetComponent<BoxContactBehavior>().boxHolder = holder;
                heldObject.GetComponent<BoxContactBehavior>().boxSFX.PlayOneShot(heldObject.GetComponent<BoxContactBehavior>().boxPickup);
                return true;
            }
        }
        return false;
    }

    public void ReleaseObject()
    {
        if(heldObject && holdingObject)
        {
            //Debug.Log("Released");
            heldObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            heldObject.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            heldObject.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            heldObject.GetComponent<BoxContactBehavior>().boxSFX.PlayOneShot(heldObject.GetComponent<BoxContactBehavior>().boxDrop);
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
            if(heldObject.GetComponent<SquashedObject>() != null && holderObject.gameObject.CompareTag("Player"))
            {
                heldObject.GetComponent<Rigidbody>().MovePosition(holderObject.position + (- holderObject.up * .25f) + (holdOffsetDir * (holdDistance + heldObject.GetComponent<BoxContactBehavior>().holdOffset)) + (-holderObject.right * (holdDistance + heldObject.GetComponent<BoxContactBehavior>().holdOffset)));
                heldObject.GetComponent<Rigidbody>().MoveRotation(holderObject.rotation);
            }
            else
            {
                heldObject.GetComponent<Rigidbody>().MovePosition(holderObject.position + (- holderObject.up * .25f) + (holdOffsetDir * (holdDistance + heldObject.GetComponent<BoxContactBehavior>().holdOffset)));
                heldObject.GetComponent<Rigidbody>().MoveRotation(holderObject.rotation);
            }
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
