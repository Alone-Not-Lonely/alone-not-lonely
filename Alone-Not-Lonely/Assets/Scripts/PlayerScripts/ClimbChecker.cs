using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbChecker : MonoBehaviour
{
    public bool inObject = false;
    [SerializeField]
    private float checkStep = .001f;
    private Transform pTransform;
    // Start is called before the first frame update
    void Start()
    {
        pTransform = GetComponentInParent<Transform>();
    }

    private void OnTriggerStay(Collider other)
    {
        inObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inObject = false; 
    }

    //should be a coroutine for performance, but testing the idea first. 
    public float checkHeight()
    {
        Debug.Log("Checking Height");
        //move to 1 height, stopping if at any point we leave the object
        while (((transform.position.y - pTransform.position.y) <= 1) && inObject)
        {
            transform.position = transform.position + Vector3.up * checkStep;
        }

        if((transform.position.y - pTransform.position.y) > 1)
        {
            //reset transform offset
            transform.position = pTransform.position + Vector3.forward;
            return 0;
        }
        //reset transform offset
        transform.position = pTransform.position + Vector3.forward;
        return (transform.position.y - pTransform.position.y);
    }
}
