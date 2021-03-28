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

    private void Start() {
        groundLocation = this.transform.position;
        raisedLocation = this.transform.position;
        raisedLocation += new Vector3(0, liftHeight, 0);
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine("ReturnToPeak");
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
}
