﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtticLadderController : MonoBehaviour
{
    //public bool boxBlockingExit = false;
    public Animator closedLadderAnimator;
    public GameObject openLadder;
    public bool canUseLadder;
    private WinCondition win;
    private Player _player;
    private void Start() {
        canUseLadder = false;
        win = GetComponent<WinCondition>();
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other) {
        //if(other.CompareTag("PhysicalGrabable"))
        //{
           // boxBlockingExit = true;
        //}

        if(canUseLadder && other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Kitchen");
        }
    }

    private void OnTriggerExit(Collider other) {
        //if(other.CompareTag("PhysicalGrabable"))
        //{
           //boxBlockingExit = false;
        //}
    }

    //public void AnimateOpenLadder()
    //{
    //    closedLadderAnimator.SetBool("OpenLadder", true);
    //}

    public void EnableDisableLadders()
    {
        win.onWin();
        closedLadderAnimator.gameObject.SetActive(false);
        openLadder.SetActive(true);
        canUseLadder = true;
    }

    //Ladder Pho-Animations

    public void open()
    {
        Debug.Log("opening");
        closedLadderAnimator.speed = .2f;
    }

    public void close() {
        Debug.Log("closing");
        //closedLadderAnimator.speed = -1f;
    }
}
