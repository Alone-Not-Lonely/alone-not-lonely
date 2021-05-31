using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LockedObject : MonoBehaviour
{
    private PlayerInventory inventory;
    public List<string> keyNames;
    public bool playerNearby = false;
    Animator _animator;
    Player playerRef;
    private ContextualUI cui;

    private AudioSource openDoor;

    void Start()
    {
        cui = GetComponent<ContextualUI>();
        playerRef = Player.instance;
        inventory = playerRef.GetComponentInChildren<PlayerInventory>();
        _animator = GetComponent<Animator>();
        openDoor = GetComponent<AudioSource>();
        if (openDoor == null)
        {
            openDoor = GetComponentInParent<AudioSource>();
        }
    }

    public void OpenAttempt()
    {
        if (!playerNearby)
        {
            return;
        }
        if (keyNames.Count == 0 || inventory.checkContents(keyNames))
        {
            openAction();
            //put opening actions here
            //remove key from inventory
            openDoor.Play();
        }
        else
        {
            Debug.Log("Closed");
            //some form of feedback perhaps
        }
    }

    public bool CheckHasKey()
    {
        return inventory.checkContents(keyNames);
    }

    private void openAction()
    {
        Debug.Log("Open");
        if(_animator != null)
        {
            _animator.SetBool("open", true);
        }
        Collider[] col = GetComponents<Collider>();
        foreach(Collider c in col)
        {
            c.enabled = false;
        }
        cui.startPoint = 2;//make sure 'e never says nothin again
        cui.endPoint = 2;
        cui.setCID(2);// a bit embarassing, but it'll have to do for now
        cui.nextPrompt();
        removeFromActions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerNearby = true;
            playerRef._actionMap.Platforming.Use.performed += grab => OpenAttempt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            removeFromActions();
        }
    }

    public void removeFromActions()
    {
        Debug.Log("Removed Actions");
        playerNearby = false;
        playerRef._actionMap.Platforming.Use.performed -= grab => OpenAttempt();
    }

    private void OnDisable() {
        playerRef._actionMap.Platforming.Use.performed -= grab => OpenAttempt();
    }

    private void OnDestroy() {
        playerRef._actionMap.Platforming.Use.performed -= grab => OpenAttempt();
    }
}
