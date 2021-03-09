using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBehavior : MonoBehaviour
{

    public Transform moveTarget;
    public float moveSpeed = 10f;
    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = (moveTarget.position - transform.position);
        transform.rotation = Quaternion.LookRotation(lookPos, Vector3.up);
        //transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed);
    }
}
