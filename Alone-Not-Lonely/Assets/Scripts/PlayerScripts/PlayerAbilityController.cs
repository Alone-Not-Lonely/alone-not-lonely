using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : MonoBehaviour
{
    //bool waitingForInput = false;
    GameObject currentGrab = null;
    [HideInInspector]
    public bool holdingObj = false;
    public Text grabText;
    public Text releaseText;

    private Player playerRef;
    public float holdDistance = 2.75f;

    // Start is called before the first frame update
    void Start()
    {
        grabText.gameObject.SetActive(false);
        releaseText.gameObject.SetActive(false);
        playerRef = (Player)FindObjectOfType(typeof(Player));
        playerRef._actionMap.Platforming.Use.performed += grab => GrabAttempt();
    }

    void Awake()
    {

    }

    void GrabAttempt()
    {
        Debug.Log("Grab Attempted");
        if (!playerRef.paused)
        {
            if (currentGrab != null && !holdingObj)//(waitingForInput && currentGrab != null)
            {
                Debug.Log("grabbed");
                if(currentGrab.GetComponent<BoxContactBehavior>().beingHeld) //this is some atrocious coding right here... restructure w inheritence later
                {
                    currentGrab.GetComponent<BoxContactBehavior>().boxHolder.GetComponent<ElevatorMonsterController>().ReleaseObject();
                    Debug.Log("elemonster release");
                }
                grabText.gameObject.SetActive(false);
                releaseText.gameObject.SetActive(true);
                //currentGrab.gameObject.transform.parent = this.transform;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //currentGrab.GetComponent<Collider>().enabled = false;//changed sphere collider to more general collider
                holdingObj = true;
                currentGrab.GetComponent<BoxContactBehavior>().beingHeld = true;
                currentGrab.GetComponent<BoxContactBehavior>().boxHolder = this.gameObject;
            }
            else if (currentGrab != null && holdingObj)//current grab redundant but colliders are wierd...
            {
                grabText.gameObject.SetActive(true);
                Debug.Log("dropped");
                //currentGrab.gameObject.transform.parent = null;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                currentGrab.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                currentGrab.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //currentGrab.GetComponent<Collider>().enabled = true;
                holdingObj = false;
                //waitingForInput = true;
                currentGrab.GetComponent<BoxContactBehavior>().beingHeld = false;
                currentGrab.GetComponent<BoxContactBehavior>().boxHolder = null;
            }
        }
    }

    public void ReleaseObject()
    {
        grabText.gameObject.SetActive(true);
        Debug.Log("dropped");
        currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        currentGrab.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentGrab.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        holdingObj = false;
        currentGrab.GetComponent<BoxContactBehavior>().beingHeld = false;
        currentGrab.GetComponent<BoxContactBehavior>().boxHolder = null;
    }

    private void FixedUpdate() {
        if(currentGrab != null && holdingObj)
        {
            currentGrab.GetComponent<Rigidbody>().MovePosition(this.transform.position + (- this.transform.up * .5f) + this.transform.forward * holdDistance);
            currentGrab.GetComponent<Rigidbody>().MoveRotation(this.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Grabable"))// && currentGrab == null)
        {
            if (!holdingObj)
            {
                grabText.gameObject.SetActive(true);
                releaseText.gameObject.SetActive(false);
            }
            currentGrab = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            if (currentGrab == collision.gameObject)
            {
                currentGrab = null;
            }
            grabText.gameObject.SetActive(false);
            releaseText.gameObject.SetActive(false);
        }
    }
}
