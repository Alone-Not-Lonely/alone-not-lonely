using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContactBehavior : MonoBehaviour
{
    public bool beingHeld;
    public GameObject boxHolder;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        if(other.gameObject.layer == 12)//elevator monster
        {

        }
        else if (other.gameObject.layer == 10)//portal monster
        {
        
        }
    }
}
