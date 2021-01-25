using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 5f;
    private int currentGoal;

    private enum State {Waiting, Moving};
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
    public float stuckTimeToMove = 3f;
    // Start is called before the first frame update
    void Start()
    {
        currentGoal = 0;
        currentState = State.Moving;
        lastTransform = this.transform.position;
        lastPortalCollider = null;
    }

    void FixedUpdate() {
        if(currentState == State.Moving && Vector3.Distance(transform.position, patrolPoints[currentGoal].position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentGoal].position, .01f * speed);
        }
        else if(currentState == State.Moving && Vector3.Distance(transform.position, patrolPoints[currentGoal].position) <= .1f)
        {
            currentState = State.Waiting;
            currentWaitTime += Time.deltaTime;
        }

        float movementLastFrame = Vector3.Distance(this.transform.position, lastTransform);
        Debug.Log("Movement last frame: " + movementLastFrame);
        if(currentState == State.Moving && movementLastFrame < speed/100f)
        {
            stuck = true;
        }
        else if(currentState == State.Moving && movementLastFrame >= speed/100f)
        {
            stuck = false;
            stuckTimer = 0f;
        }

        if(stuck && stuckTimer < stuckTimeToMove)
        {
            stuckTimer += Time.deltaTime;
        }
        else if(stuck && stuckTimer >= stuckTimeToMove)
        {
            currentGoal++;
            if(currentGoal >= patrolPoints.Length)
            {
                currentGoal = currentGoal % patrolPoints.Length;
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
            if(currentGoal >= patrolPoints.Length)
            {
                currentGoal = currentGoal % patrolPoints.Length;
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
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            this.transform.position = other.GetComponent<PortalController>().partnerPortal.transform.position;
            inColliderCooldown = true;
            lastPortalCollider = other.GetComponent<PortalController>().partnerPortal.gameObject.GetComponent<Collider>();
            lastPortalCollider.enabled = false;
        }
    }
}
