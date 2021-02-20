using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContactBehavior : MonoBehaviour
{
    public bool beingHeld;
    public GameObject boxHolder;

    private void Start() {
        beingHeld = false;
        boxHolder = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if(boxHolder != null && (other.gameObject.CompareTag("Portal")|| other.gameObject.CompareTag("BackPortal")))
        {
            try{
                boxHolder.GetComponent<PatrolPointsController>().TurnAround();
                boxHolder.GetComponent<Grabber>().ReleaseObject();
            }
            catch{}
            
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(boxHolder != null && (other.gameObject.CompareTag("Portal")|| other.gameObject.CompareTag("BackPortal")))
        {
            try{
                boxHolder.GetComponent<PatrolPointsController>().TurnAround();
                boxHolder.GetComponent<Grabber>().ReleaseObject();
            }
            catch{}
            
        }
    }
}
