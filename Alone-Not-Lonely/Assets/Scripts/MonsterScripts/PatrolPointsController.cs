using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : Grabber
{
    public int loopPoint;
    public List<Transform> patrolPoints; // stores portal coordinates now
    public float speed = 5f;
    private int currentGoal;

    private enum State {Waiting, Moving, Collided};
    private State currentState;

    private float waitTime = 3f;
    private float currentWaitTime = 0;

    private float colliderCooldown = 2f;
    private float currentColliderCooldown = 0f;
    private bool inColliderCooldown = false;
    private Collider lastPortalCollider;

    private Vector3 lastTransform;
    private bool stuck = false;
    private float stuckTimer;
    public float stuckTimeToMove = 1f;
    public float allowableMoveMargin = .1f;
    public float portalSpawnOffset = .3f;

    private Rigidbody thisRB;

    private float directionDot; //updated in fixedUpdate
    private Vector3 movementLastFrame;
    public GameObject activePortal1;
    public GameObject activePortal2;

    void Start()
    {
        currentGoal = 0;
        currentState = State.Moving;
        lastTransform = this.transform.position;
        lastPortalCollider = null;
        thisRB = GetComponent<Rigidbody>();
        activePortal1 = patrolPoints[0].gameObject;
        activePortal2 = patrolPoints[1].gameObject;
        UpdatePortalSizes();
    }

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

        if(currentState == State.Waiting && currentWaitTime < waitTime)// we're not doing waiting anymore but I'm leaving this here in case removing it breaks the game
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

        if(inColliderCooldown && currentColliderCooldown < colliderCooldown) //can't collide back into the portal it just exited
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

        if(currentState == State.Moving && Vector3.Distance(transform.position, patrolPoints[currentGoal].position) > .1f) //move monster towards goal
        {
            thisRB.MovePosition(Vector3.MoveTowards(transform.position, patrolPoints[currentGoal].position, .01f * speed));
        }
        else if(currentState == State.Moving && Vector3.Distance(transform.position, patrolPoints[currentGoal].position) <= .1f) //more waiting code that im afraid to delete ;_;
        {
            currentState = State.Waiting;
            currentWaitTime += Time.deltaTime;
        }
        FixedUpdate(this.transform, movementLastFrame.normalized); //update abstract parent
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
                    currentGoal = loopPoint;
                }
                this.gameObject.SetActive(true);
                activePortal1 = patrolPoints[currentGoal].gameObject;
                activePortal2 = patrolPoints[currentGoal + 1].gameObject;
                UpdatePortalSizes();
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
        Debug.Log("Monster entered a collision");
        if(other.gameObject.CompareTag("Grabable")) //trying to get something going here wrt knocking the box out of the lift monster's grasp
        {
            //checks to see if the grabbable thing is a box and it isn't being held
            //This was added to stop monsters from snatching the box from a while away
            BoxContactBehavior box = other.gameObject.GetComponent<BoxContactBehavior>();
            if (box != null && box.boxHolder == null)
            {
                GrabAttempt(other.gameObject, this.gameObject);
            }
        }
    }

    void UpdatePortalSizes()
    {
        foreach(Transform portal in patrolPoints)
        {
            if(portal.gameObject == activePortal1 || portal.gameObject == activePortal2)
            {
                StartCoroutine("GrowPortal", portal);
            }
            else
            {
                StartCoroutine("ShrinkPortal", portal);
            }
        }
    }

    IEnumerator ShrinkPortal(Transform portal)
    {
        while(portal.localScale.x > .25f)
        {
            portal.localScale -= new Vector3(.01f, .01f, .01f);
            yield return new WaitForSeconds(.001f);
        }
        yield break;
    }
    IEnumerator GrowPortal(Transform portal)
    {
        while(portal.localScale.x < .37f)
        {
            portal.localScale += new Vector3(.01f, .01f, .01f);
            yield return new WaitForSeconds(.001f);
        }
        yield break;
    }
}
