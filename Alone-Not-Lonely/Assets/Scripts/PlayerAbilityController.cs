﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField]
    bool waitingForInput = false;
    [SerializeField]
    GameObject currentGrab = null;
    [SerializeField]
    bool holdingObj = false;
    public Text grabText;
    public Text releaseText;

    private Player playerRef;

    // Start is called before the first frame update
    void Start()
    {
        grabText.gameObject.SetActive(false);
        releaseText.gameObject.SetActive(false);
    }

    void Awake()
    {
        playerRef = (Player)FindObjectOfType(typeof(Player));
        playerRef._actionMap.Platforming.Use.performed += grab => GrabAttempt();
    }

    void GrabAttempt()
    {
        if (!playerRef.paused)
        {
            if (waitingForInput && currentGrab != null)
            {
                waitingForInput = false;
                Debug.Log("grabbed");
                grabText.gameObject.SetActive(false);
                releaseText.gameObject.SetActive(true);
                currentGrab.gameObject.transform.parent = this.transform;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                currentGrab.GetComponent<Collider>().enabled = false;//changed sphere collider to more general collider
                holdingObj = true;
                
            }
            else if (holdingObj)
            {
                grabText.gameObject.SetActive(true);
                Debug.Log("dropped");
                currentGrab.gameObject.transform.parent = null;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                currentGrab.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                currentGrab.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                currentGrab.GetComponent<Collider>().enabled = true;
                holdingObj = false;
                waitingForInput = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!holdingObj && collision.gameObject.CompareTag("Grabable"))
        {  
            if(!waitingForInput)
            {
                grabText.gameObject.SetActive(true);
                releaseText.gameObject.SetActive(false);
                currentGrab = collision.gameObject;
            }
            waitingForInput = true;
        }
    }

    private void OnTriggerExit(Collider collision) 
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            grabText.gameObject.SetActive(false);
            waitingForInput = false; // - A
        }
    }
}
