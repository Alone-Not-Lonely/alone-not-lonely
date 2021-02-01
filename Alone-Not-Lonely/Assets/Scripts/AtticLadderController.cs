using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtticLadderController : MonoBehaviour
{
    public bool boxBlockingExit = false;
    public Animator closedLadderAnimator;
    public GameObject openLadder;
    public bool canUseLadder;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Grabable"))
        {
            boxBlockingExit = true;
        }
        else if(canUseLadder && other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndPlaytest");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Grabable"))
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
