using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : Grabber
{
    public List<Transform> patrolPoints;
    public float speed = 5f;
    public int currentGoal;

    public enum State {Waiting, Moving, Collided};
    public State currentState;

    private float waitTime = 3f;
    private float currentWaitTime = 0;

    private float colliderCooldown = 2f;
    private float currentColliderCooldown = 0f;
    private bool inColliderCooldown = false;
    private Collider lastPortalCollider;

    private Vector3 lastTransform;
    public bool stuck = false;
    private float stuckTimer;
    public float stuckTimeToMove = 1f;
    public float allowableMoveMargin = .1f;
    public float portalSpawnOffset = .3f;

    private Rigidbody thisRB;

    public float launchForce = .00001f;
    // Start is called before the first frame update
    void Start()
    {
        currentGoal = 0;
        currentState = State.Moving;
        lastTransform = this.transform.position;
        lastPortalCollider = null;
        thisRB = GetComponent<Rigidbody>();
    }

    private float directionDot; //updated in fixedUpdate
    private Vector3 movementLastFrame;
    void FixedUpdate() {
        movementLastFrame = this.transform.position - lastTransform;//Total movement after one frame
        directionDot = Vector3.Dot(Vector3.Normalize(movementLastFrame), Vector3.Normalize(patrolPoints[currentGoal].position - transform.position));//dot product of last frame movement and movement to goal

        //unsticks if monster's way has been cleared
        if (stuck && currentState == State.Moving && directionDot >= allowableMoveMargin)//old evaluation: movementLastFrame >= (.01f * speed) - allowableMoveMargin
        {
            stuck = false;
            stuckTimer = 0f;
        }

        //constinue being stuck
        if(stuck && stuckTimer < stuckTimeToMove)
        {
            stuckTimer += Time.deltaTime;
        }
        else if(stuck && stuckTimer >= stuckTimeToMove) // turn around to get unstuck
        {
            TurnAround();
        }

        if(currentState == State.Waiting && currentWaitTime < waitTime)
        {
            currentWaitTime += Time.deltaTime;
        }
        else if(currentState == State.Waiting && currentWaitTime >= waitTime)
        {
            currentState = State.Moving;
            currentWaitTime = 0f;
            currentGoal++;
            if(currentGoal >= patrolPoints.Count)
            {
                currentGoal = currentGoal % patrolPoints.Count;
            }
        }

        if(inColliderCooldown && currentColliderCooldown < colliderCooldown)
        {
            currentColliderCooldown += Time.deltaTime;
        }
        else if(inColliderCooldown && currentColliderCooldown >= colliderCooldown)
        {
            lastPortalCollider.enabled = true;
            lastPortalCollider = null;
            currentColliderCooldown = 0f;
            inColliderCooldown = false;
        }
        lastTransform = this.transform.position;

        if(currentState == State.Moving && Vector3.Distance(transform.position, patrolPoints[currentGoal].position) > .1f)
        {
            //transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentGoal].position, .01f * speed);
            thisRB.MovePosition(Vector3.MoveTowards(transform.position, patrolPoints[currentGoal].position, .01f * speed));
        }
        else if(currentState == State.Moving && Vector3.Distance(transform.position, patrolPoints[currentGoal].position) <= .1f)
        {
            currentState = State.Waiting;
            currentWaitTime += Time.deltaTime;
        }
        FixedUpdate(this.transform, movementLastFrame.normalized);
    }

    public void TurnAround()
    {
        currentGoal++;
        currentState = State.Moving;
        thisRB.velocity = Vector3.zero;
        thisRB.angularVelocity = Vector3.zero;
        if(currentGoal >= patrolPoints.Count)
        {
            currentGoal = currentGoal % patrolPoints.Count;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!holdingObject)
        {
            if(other.CompareTag("Portal"))
            {
                this.gameObject.SetActive(false);
                GameObject partnerPortal = other.GetComponent<PortalController>().partnerPortal;
                this.transform.position = partnerPortal.transform.position + (partnerPortal.transform.forward * portalSpawnOffset);
                inColliderCooldown = true;
                lastPortalCollider = partnerPortal.gameObject.GetComponent<Collider>();
                lastPortalCollider.enabled = false;
                thisRB.velocity = Vector3.zero;
                thisRB.angularVelocity = Vector3.zero;
                this.gameObject.SetActive(true);
            }
            else if(other.CompareTag("BackPortal"))
            {
                this.gameObject.SetActive(false);
                GameObject partnerPortal = other.GetComponent<PortalController>().partnerPortal;
                this.transform.position = partnerPortal.transform.position + (partnerPortal.transform.forward * portalSpawnOffset);
                inColliderCooldown = true;
                lastPortalCollider = partnerPortal.gameObject.GetComponent<Collider>();
                lastPortalCollider.enabled = false;
                thisRB.velocity = Vector3.zero;
                thisRB.angularVelocity = Vector3.zero;
                //advance current patrolpoint
                currentGoal ++;
                if(currentGoal >= patrolPoints.Count)
                {
                    currentGoal = 6;
                }
                this.gameObject.SetActive(true);
            }
        }
        else
        {
            if(other.CompareTag("Portal") || other.CompareTag("BackPortal"))
            {
                //TurnAround();
                //heldObject.transform.Translate
                //ReleaseObject();
            }
            
            //stuck inherently cause you will not be moving
            /*float movementLastFrame = Vector3.Distance(this.transform.position, lastTransform);
            if(!stuck && currentState == State.Moving && directionDot < allowableMoveMargin)
            {
                stuck = true;
            }*/
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        float movementLastFrame = Vector3.Distance(this.transform.position, lastTransform);
        if(!stuck && currentState == State.Moving && directionDot < allowableMoveMargin)
        {
            stuck = true;
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable")) //trying to get something going here wrt knocking the box out of the lift monster's grasp
        {
            GrabAttempt(other.gameObject, this.gameObject);
            /*var collidedObject = other.gameObject.GetComponent<BoxContactBehavior>();
            if(collidedObject.beingHeld)
            {
                try{
                    if(collidedObject.boxHolder.GetComponent<ElevatorMonsterController>())
                    {
                        collidedObject.boxHolder.GetComponent<Collider>().enabled = false;
                        collidedObject.boxHolder.GetComponent<ElevatorMonsterController>().ReleaseObject();
                        collidedObject.gameObject.GetComponent<Rigidbody>().AddForce(other.GetContact(0).normal.normalized * -launchForce);
                        collidedObject.boxHolder.GetComponent<Collider>().enabled = true;
                    }
                }
                catch{
                    //if its not the elevator monster do nothing
                }
                
            }*/
        }
    }
}
