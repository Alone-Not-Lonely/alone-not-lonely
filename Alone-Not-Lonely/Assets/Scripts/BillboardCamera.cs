using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCamera : MonoBehaviour
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
        this.transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, this.transform.eulerAngles.z, this.transform.eulerAngles.z);
    }
}
