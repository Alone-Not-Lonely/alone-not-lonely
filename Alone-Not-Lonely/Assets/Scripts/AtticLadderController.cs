using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticLadderController : MonoBehaviour
{
    public bool boxBlockingExit = false;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Grabable"))
        {
            boxBlockingExit = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Grabable"))
        {
            boxBlockingExit = false;
        }
    }
}
