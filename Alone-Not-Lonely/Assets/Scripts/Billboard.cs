using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;
    
    void Start()
    {
        target = Camera.main.transform; //cace the transform of the camera
    }

    void LateUpdate() 
    {
        Transform thisTransform = this.transform;
        //thisTransform.LookAt(target);
        Quaternion rotation = thisTransform.rotation;
        rotation.SetLookRotation((target.position - thisTransform.position).normalized);
        //Vector3 rot = thisTransform.eulerAngles;
        this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, rotation.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
