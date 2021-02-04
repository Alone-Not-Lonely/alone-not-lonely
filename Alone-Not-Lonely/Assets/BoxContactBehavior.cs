using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContactBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
    }
}
