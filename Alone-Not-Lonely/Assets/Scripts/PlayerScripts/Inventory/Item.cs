using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string key;
    public int ID;
    //public Sprite representation;
    private PlayerInventory inventory;
    bool playerNearby = false;
    Player playerRef;

    void Start()
    {
        playerRef = (Player)FindObjectOfType(typeof(Player));
        inventory = playerRef.GetComponentInChildren<PlayerInventory>();
        playerRef._actionMap.Platforming.Use.performed += grab => pickUp();
        if(key != "Puzzle Piece"){
            ID = -1;
        }
    }

    void pickUp()
    {
        if (!playerNearby)
        {
            return;
            
        }

        inventory.addItem(this);
        this.gameObject.SetActive(false);//cannot destroy, causes scripting complications
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNearby = false;
        }
    }

    private void OnDisable()
    {
        playerNearby = false;
    }
}
