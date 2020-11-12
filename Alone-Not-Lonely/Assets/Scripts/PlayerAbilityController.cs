using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    bool waitingForInput = false;
    GameObject currentGrab = null;
    bool holdingObj = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingForInput && currentGrab != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("grabbed");
            currentGrab.gameObject.transform.parent = this.transform;
            currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            currentGrab.GetComponent<SphereCollider>().enabled = false;
            holdingObj = true;
            waitingForInput = false;
        }
        else if(holdingObj && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("dropped");
            currentGrab.gameObject.transform.parent = null;
            currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            currentGrab.GetComponent<SphereCollider>().enabled = true;
            holdingObj = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            currentGrab = collision.gameObject;
            waitingForInput = true;
            Debug.Log("waiting for input");
        }
    }
}
