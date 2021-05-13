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

    private float colliderCooldown = 2f;
    private float currentColliderCooldown = 0f;
    public bool inColliderCooldown = false;
    private Collider lastPortalCollider;

    private Vector3 lastTransform;
    public float portalSpawnOffset = .3f;

    private Rigidbody thisRB;

    private float directionDot; //updated in fixedUpdate
    public Vector3 movementLastFrame;
    public GameObject activePortal1;
    public GameObject activePortal2;

    private bool collidingWithGrabable = false;

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
    }

    private void OnCollisionStay(Collision other) 
    {

    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable")) //trying to get something going here wrt knocking the box out of the lift monster's grasp
        {
            //checks to see if the grabbable thing is a box and it isn't being held
            //This was added to stop monsters from snatching the box from a while away
            BoxContactBehavior box = other.gameObject.GetComponent<BoxContactBehavior>();
            if (!this.holdingObject && box != null && box.boxHolder == null)
            {
                Debug.Log("GRABBING MINE");
                GrabAttempt(other.gameObject, this.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision other) 
    {
    }

    void UpdatePortalSizes()
    {
        foreach(Transform portal in patrolPoints)
        {
            if(portal.gameObject == activePortal1 || portal.gameObject == activePortal2)
            {
                StartCoroutine("GrowPortal", portal);
                portal.GetComponent<SpriteRenderer>().color = new Color(160f/255f, 0, 22f/255f);
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
