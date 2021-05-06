using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuzzleBoxAnimation : MonoBehaviour
{
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            if(other.gameObject.GetComponent<Grabber>().holdingObject)
            {
                this.GetComponent<Animator>().SetBool("StartAnim", true);
            }
        }
    }
}
