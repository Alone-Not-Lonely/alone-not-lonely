using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : MonoBehaviour
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
    void FixedUpdate() {
        Vector3 movementLastFrame = this.transform.position - lastTransform;
        directionDot = Vector3.Dot(Vector3.Normalize(movementLastFrame), Vector3.Normalize(patrolPoints[currentGoal].position - transform.position));//dot product of last frame movement and movement to goal
        if(stuck && currentState == State.Moving && directionDot >= allowableMoveMargin)//old evaluation: movementLastFrame >= (.01f * speed) - allowableMoveMargin
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
            currentGoal++;
            currentState = State.Moving;
            thisRB.velocity = Vector3.zero;
            thisRB.angularVelocity = Vector3.zero;
            if(currentGoal >= patrolPoints.Count)
            {
                currentGoal = currentGoal % patrolPoints.Count;
            }
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
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            GameObject partnerPortal = other.GetComponent<PortalController>().partnerPortal;
            this.transform.position = partnerPortal.transform.position + (partnerPortal.transform.forward * portalSpawnOffset);
            inColliderCooldown = true;
            lastPortalCollider = partnerPortal.gameObject.GetComponent<Collider>();
            lastPortalCollider.enabled = false;
            thisRB.velocity = Vector3.zero;
            thisRB.angularVelocity = Vector3.zero;
        }
        else if(other.CompareTag("BackPortal"))
        {
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
}
