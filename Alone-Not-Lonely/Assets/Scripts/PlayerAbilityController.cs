using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : MonoBehaviour
{
    //bool waitingForInput = false;
    GameObject currentGrab = null;
    bool holdingObj = false;
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
                //Debug.Log("grabbed");
                grabText.gameObject.SetActive(false);
                releaseText.gameObject.SetActive(true);
                //currentGrab.gameObject.transform.parent = this.transform;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //currentGrab.GetComponent<Collider>().enabled = false;//changed sphere collider to more general collider
                holdingObj = true;
            }
            else if (currentGrab != null && holdingObj)//current grab redundant but colliders are wierd...
            {
                grabText.gameObject.SetActive(true);
                Debug.Log("dropped");
                //currentGrab.gameObject.transform.parent = null;
                //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                currentGrab.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                //currentGrab.GetComponent<Collider>().enabled = true;
                holdingObj = false;
                //waitingForInput = true;
            }
        }
    }

    private void FixedUpdate() {
        if(currentGrab != null && holdingObj)
        {
            currentGrab.GetComponent<Rigidbody>().MovePosition(this.transform.position + - this.transform.up + this.transform.forward * holdDistance);
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
        }
    }
}
