using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtticLadderController : MonoBehaviour
{
    //public bool boxBlockingExit = false;
    public Animator closedLadderAnimator;
    public GameObject openLadder;
    public bool canUseLadder;
    public float lOspeed = .1f, lCspeed = 1f;
    //private WinCondition win;
    private Player _player;

    private void Start() {
        canUseLadder = false;

        //win = GetComponent<WinCondition>();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {

        Debug.Log("playback speed: " + closedLadderAnimator.speed);
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
        //win.onWin();
        //closedLadderAnimator.gameObject.SetActive(false);
        openLadder.SetActive(true);
        canUseLadder = true;
    }

    //Ladder Pho-Animations

    public void open()
    {
        Debug.Log("opening");
       // closedLadderAnimator.SetBool("opening", true);
        closedLadderAnimator.SetFloat("anim_speed", lOspeed);
    }

    public void close() {
        Debug.Log("closing");
        //closedLadderAnimator.SetBool("opening", false);
        closedLadderAnimator.SetFloat("anim_speed", -lCspeed);
    }
}
