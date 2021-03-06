using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContactBehavior : MonoBehaviour
{
    public bool beingHeld;
    public GameObject boxHolder;
    public AudioSource boxSFX;
    public AudioClip boxPickup;
    public AudioClip boxDrop;

    private void Start() {
        beingHeld = false;
        boxHolder = null;
        boxSFX = this.GetComponent<AudioSource>();
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
