using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContactBehavior : MonoBehaviour
{
    public bool beingHeld;
    public GameObject boxHolder;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12)//elevator monster
        {

        }
        else if (other.gameObject.layer == 10)//portal monster
        {
        
        }
        if(boxHolder != null && (other.gameObject.CompareTag("Portal")|| other.gameObject.CompareTag("BackPortal")))
        {
            boxHolder.GetComponent<Grabber>().ReleaseObject();
            try
            {
                boxHolder.GetComponent<PatrolPointsController>().TurnAround();
            }
            catch
            {

            }
        }
    }
}
