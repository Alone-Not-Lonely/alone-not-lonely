﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : MonoBehaviour
{
    bool waitingForInput = false;
    GameObject currentGrab = null;
    bool holdingObj = false;
    public Text grabText;
    public Text releaseText;

    private Player playerRef;

    // Start is called before the first frame update
    void Start()
    {
        grabText.gameObject.SetActive(false);
        releaseText.gameObject.SetActive(false);
        playerRef = (Player)FindObjectOfType(typeof(Player));
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
                Debug.Log("grabbed");
                grabText.gameObject.SetActive(false);
                releaseText.gameObject.SetActive(true);
                currentGrab.gameObject.transform.parent = this.transform;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                currentGrab.GetComponent<SphereCollider>().enabled = false;
                holdingObj = true;
                waitingForInput = false;
            }
            else if (holdingObj)
            {
                grabText.gameObject.SetActive(true);
                Debug.Log("dropped");
                currentGrab.gameObject.transform.parent = null;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                currentGrab.GetComponent<SphereCollider>().enabled = true;
                holdingObj = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            grabText.gameObject.SetActive(true);
            releaseText.gameObject.SetActive(false);
            currentGrab = collision.gameObject;
            waitingForInput = true;
            Debug.Log("waiting for input");
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
