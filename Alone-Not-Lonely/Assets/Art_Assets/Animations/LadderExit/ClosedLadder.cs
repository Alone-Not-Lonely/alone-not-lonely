using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedLadder : MonoBehaviour
{
    private AtticLadderController ladderController;
    void Start() 
    {
        ladderController = transform.parent.GetComponent<AtticLadderController>();
    }
    
    void OnAnimationEnded()
    {
        ladderController.EnableDisableLadders();
    }
}
