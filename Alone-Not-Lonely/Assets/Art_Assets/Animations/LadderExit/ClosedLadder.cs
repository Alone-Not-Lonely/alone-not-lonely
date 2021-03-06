using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedLadder : MonoBehaviour
{
    private AtticLadderController ladderController;
    AudioSource doorBreak;
    void Start() 
    {
        ladderController = transform.parent.GetComponent<AtticLadderController>();
        doorBreak = GetComponent<AudioSource>();
    }
    
    void OnAnimationEnded()
    {
        doorBreak.Play();
        ladderController.EnableDisableLadders();
    }
}
