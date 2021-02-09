using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMonsterController : MonoBehaviour
{
    public GameObject heldObject;
    public Vector3 topPoint;
    public float height = 2f;
    public float speed = 1f;

    public bool holdingObject;
    void Start()
    {
        topPoint = this.transform.position + (this.transform.up * height);
        holdingObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingObject && Vector3.Distance(heldObject.transform.position, topPoint) > .1f)
        {
            heldObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(heldObject.transform.position, topPoint, .01f * speed));
            //heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, topPoint, .01f * speed);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Grabable"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject = other.gameObject;
            holdingObject = true;
            //other.gameObject.transform.parent = this.transform;
        }
    }

    /*void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Grabable"))
        {
            other.gameObject.GetComponent<Rigidbody>().WakeUp();
            heldObject = null;
            holdingObject = false;
            //other.gameObject.transform.parent = null;
        }
    }*/
}
