using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : Grabber
{
    public GameObject currentGrab = null;
    public Text grabText;
    public Text releaseText;

    private Player playerRef;
    private BoxCollider collideWithWalls;
    void Start()
    {
        //grabText.gameObject.SetActive(false);
        //releaseText.gameObject.SetActive(false);
        playerRef = Player.instance;
        playerRef._actionMap.ViewingObject.Disable();
        playerRef._actionMap.Platforming.Use.performed += grab => PlayerGrab();
    }
    private void OnEnable() {
        playerRef = Player.instance;
        if(playerRef != null)
        {
            //playerRef.InstantiateControls();
            //playerRef._actionMap.ViewingObject.Disable();
            //playerRef._actionMap.Platforming.Use.performed += grab => PlayerGrab();
        }
    }

    private void OnDisable()
    {
        //playerRef._actionMap.ViewingObject.Disable();
        //playerRef._actionMap.Platforming.Use.performed -= grab => PlayerGrab();
    }

    void PlayerGrab()
    {
        if (!playerRef.paused)
        {
            if (currentGrab != null && !base.holdingObject)
            {
                if(Vector3.Distance(this.transform.position, currentGrab.transform.position) > 5) //please god no more teleporting
                {
                    return;
                }
                //grabText.gameObject.SetActive(false);
                //releaseText.gameObject.SetActive(true);
                GrabAttempt(currentGrab, this.gameObject);
                if (currentGrab.GetComponent<ContextualUI>()!=null)
                {
                    currentGrab.GetComponent<ContextualUI>().nextPrompt();
                }

            }
            else if (base.holdingObject)//current grab redundant but colliders are wierd...
            {
                if (currentGrab.GetComponent<ContextualUI>() != null)
                {
                    currentGrab.GetComponent<ContextualUI>().nextPrompt();
                }
                ReleaseObject();
            }
        }
    }

    private void FixedUpdate() {
        FixedUpdate(this.transform, this.transform.forward);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            currentGrab = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(!base.holdingObject && collision.gameObject.CompareTag("Grabable"))
        {
            if (currentGrab == collision.gameObject)
            {
                currentGrab = null;
            }
        }
    }
}
