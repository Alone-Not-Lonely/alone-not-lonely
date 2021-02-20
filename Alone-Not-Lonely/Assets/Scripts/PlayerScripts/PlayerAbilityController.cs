using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : Grabber
{
    public GameObject currentGrab = null;
    [HideInInspector]
    public Text grabText;
    public Text releaseText;

    private Player playerRef;
    void Start()
    {
        grabText.gameObject.SetActive(false);
        releaseText.gameObject.SetActive(false);
        playerRef = (Player)FindObjectOfType(typeof(Player));
        playerRef._actionMap.Platforming.Use.performed += grab => GrabAttempt();
    }

    void GrabAttempt()
    {
        if (!playerRef.paused)
        {
            if (currentGrab != null && !base.holdingObject)//(waitingForInput && currentGrab != null)
            {
                grabText.gameObject.SetActive(false);
                releaseText.gameObject.SetActive(true);
                base.GrabAttempt(currentGrab, this.gameObject);
            }
            else if (currentGrab != null && base.holdingObject)//current grab redundant but colliders are wierd...
            {
                grabText.gameObject.SetActive(true);
                base.ReleaseObject();
            }
        }
    }

    private void FixedUpdate() {
        base.FixedUpdate(this.transform, this.transform.forward);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Grabable"))
        {
            if (!base.holdingObject)
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
