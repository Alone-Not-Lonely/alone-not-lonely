using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : MonoBehaviour
{
    private PlayerInventory inventory;
    public List<string> keyNames;
    bool playerNearby = false;
    Animator _animator;
    Player playerRef;

    void Start()
    {
        playerRef = Player.instance;
        inventory = playerRef.GetComponentInChildren<PlayerInventory>();
        _animator = GetComponent<Animator>();
    }

    void OpenAttempt()
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
        //GetComponent<ContextualUI>().conText.text = "";
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
            playerNearby = false;
            playerRef._actionMap.Platforming.Use.performed -= grab => OpenAttempt();
        }
    }
}
