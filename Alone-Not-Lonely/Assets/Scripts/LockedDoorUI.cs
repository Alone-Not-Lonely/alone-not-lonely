using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LockedObject))]
public class LockedDoorUI : MonoBehaviour
{
    private LockedObject doorScript;

    void Start() {
        doorScript = GetComponent<LockedObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //conditionMet = doorScript.CheckHasKey();
        }
        //base.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other) {
        //base.OnTriggerExit(other);
    }

    void OnDisable() {
        //base.OnDisable();
    }

    void OnEnable() {
        //base.OnEnable();
    }
}
