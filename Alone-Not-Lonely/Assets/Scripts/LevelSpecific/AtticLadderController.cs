﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtticLadderController : MonoBehaviour
{
    public bool boxBlockingExit = false;
    public Animator closedLadderAnimator;
    public GameObject openLadder;
    public bool canUseLadder;
    private void Start() {
        canUseLadder = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PhysicalGrabable"))
        {
            boxBlockingExit = true;
        }
        if(canUseLadder && other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Kitchen");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("PhysicalGrabable"))
        {
            boxBlockingExit = false;
        }
    }

    public void AnimateOpenLadder()
    {
        closedLadderAnimator.SetBool("OpenLadder", true);
    }

    public void EnableDisableLadders()
    {
        closedLadderAnimator.gameObject.SetActive(false);
        openLadder.SetActive(true);
        canUseLadder = true;
    }
}
