using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashMonsterController : MonoBehaviour
{
    public float timeToSmash = 5f;
    public float smashCountdown = 0;
    private bool smashing = false;
    private Vector3 groundLocation;
    private Vector3 raisedLocation;
    private Rigidbody rb;
    public float liftHeight = 5f;
    public float deltaPos = .25f;

    public GameObject mostRecentSquash = null;
    public float timeToUnSquash = 5f;
    private float currentSquashTime = 0f;

    private void Start() {
        groundLocation = this.transform.position;
        raisedLocation = this.transform.position;
        raisedLocation += new Vector3(0, liftHeight, 0);
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update() {
        if(!smashing && smashCountdown < timeToSmash)
        {
            smashCountdown += Time.deltaTime;
        }
        else if(!smashing && smashCountdown >= timeToSmash)
        {
            smashing = true;
            smashCountdown = 0f;
        }
        if(mostRecentSquash != null && currentSquashTime < timeToUnSquash)
        {
            currentSquashTime += Time.deltaTime;
        }
        else if(mostRecentSquash != null && currentSquashTime >= timeToUnSquash)
        {
            mostRecentSquash.GetComponent<BoxSquashBehavior>().UnSquash();
            GameObject unsquashedObj = Instantiate(mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant, mostRecentSquash.transform.position, Quaternion.identity);
            unsquashedObj.GetComponent<BoxSquashBehavior>().squashed = false;
            unsquashedObj.GetComponent<BoxSquashBehavior>().squashedVariant = mostRecentSquash.GetComponent<BoxSquashBehavior>().squashedVariant;
            unsquashedObj.GetComponent<BoxSquashBehavior>().regularVariant = mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant;
            Destroy(mostRecentSquash);
            mostRecentSquash = null;
            currentSquashTime = 0f;
        }
    }

    private void FixedUpdate() {
        if(smashing)
        {
            rb.MovePosition(this.transform.position - new Vector3(0,deltaPos * Time.deltaTime, 0));
            deltaPos += .1f;
            if(transform.position.y < groundLocation.y)
            {
                smashing = false;
                deltaPos = 1f;
            }
        }
        else
        {
            rb.MovePosition(this.transform.position - new Vector3(0,-deltaPos * Time.deltaTime, 0));
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(smashing && other.gameObject.CompareTag("Grabable"))
        {
            other.gameObject.GetComponentInParent<BoxSquashBehavior>().Squash();
            mostRecentSquash = Instantiate(other.gameObject.GetComponentInParent<BoxSquashBehavior>().squashedVariant, other.gameObject.transform.position, Quaternion.identity);
            mostRecentSquash.GetComponent<BoxSquashBehavior>().squashed = true;
            mostRecentSquash.GetComponent<BoxSquashBehavior>().squashedVariant = other.gameObject.GetComponentInParent<BoxSquashBehavior>().squashedVariant;
            mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant = other.gameObject.GetComponentInParent<BoxSquashBehavior>().regularVariant;
            Destroy(other.gameObject);
        }
    }
}
