using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject : MonoBehaviour
{
    private PlayerInventory inventory;
    public List<Item> keys;
    bool playerNearby = false;
    Animator _animator;

    void Start()
    {
        Player playerRef = (Player)FindObjectOfType(typeof(Player));
        inventory = playerRef.GetComponentInChildren<PlayerInventory>();
        playerRef._actionMap.Platforming.Use.performed += grab => OpenAttempt();
        _animator = GetComponent<Animator>();
    }

    void OpenAttempt()
    {
        if (!playerNearby)
        {
            return;
        }
        if (inventory.checkContents(keys))
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

    private void openAction()
    {
        Debug.Log("Open");
        _animator.SetBool("open", true);
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerNearby = true;
        }
    }

    private void OnTriggeExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNearby = false;
        }
    }
}
