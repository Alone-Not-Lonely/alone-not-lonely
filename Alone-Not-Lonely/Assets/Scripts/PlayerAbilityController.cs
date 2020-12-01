using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityController : MonoBehaviour
{
    bool waitingForInput = false;
    GameObject currentGrab = null;
    bool holdingObj = false;
    public Text grabText;
    public Text releaseText;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private Vector3 camSpawnPosition;
    private Quaternion camSpawnRotation;
    // Start is called before the first frame update
    void Start()
    {
        grabText.gameObject.SetActive(false);
        releaseText.gameObject.SetActive(false);

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        camSpawnPosition = Camera.main.gameObject.transform.position;
        camSpawnRotation = Camera.main.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingForInput && currentGrab != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("grabbed");
            grabText.gameObject.SetActive(false);
            releaseText.gameObject.SetActive(true);
            currentGrab.gameObject.transform.parent = this.transform;
            //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            //currentGrab.GetComponent<SphereCollider>().enabled = false;
            holdingObj = true;
            waitingForInput = false;
        }
        else if(holdingObj && Input.GetKeyDown(KeyCode.E))
        {
            grabText.gameObject.SetActive(true);
            Debug.Log("dropped");
            currentGrab.gameObject.transform.parent = null;
            //currentGrab.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            //currentGrab.GetComponent<SphereCollider>().enabled = true;
            holdingObj = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            grabText.gameObject.SetActive(true);
            releaseText.gameObject.SetActive(false);
            currentGrab = collision.gameObject;
            waitingForInput = true;
            Debug.Log("waiting for input");
        }
        if(collision.gameObject.CompareTag("Deadly"))
        {
            this.gameObject.SetActive(false);
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;
            Camera.main.gameObject.transform.position = camSpawnPosition;
            Camera.main.gameObject.transform.rotation = camSpawnRotation;
            this.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision) 
    {
        if(collision.gameObject.CompareTag("Grabable"))
        {
            grabText.gameObject.SetActive(false);
        }
    }
}
