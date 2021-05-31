using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    public string key;
    public int ID;
    //public Sprite representation;
    private PlayerInventory inventory;
    bool playerNearby = false;
    Player playerRef;
    //[SerializeField]
    public ContextualUI doorCU;

    private AudioSource itemPickup;

    void Start()
    {
        bool destroyThis = false;
        foreach(string i in PlayerInventory.instance.items)
        {
            if(i != "Puzzle Piece" && i == this.key)
            {
                Debug.Log("Destroying " + this.key);
                destroyThis = true;
            }
        }
        if(destroyThis)
            Destroy(this.gameObject);
        playerRef = Player.instance;
        inventory = playerRef.GetComponentInChildren<PlayerInventory>();
        playerRef._actionMap.Platforming.Use.performed += grab => pickUp();
        if(key != "Puzzle Piece"){
            ID = -1;
        }
        itemPickup = GetComponent<AudioSource>();
        itemPickup.loop = false;
        itemPickup.playOnAwake = false;
    }

    void pickUp()
    {
        if (!playerNearby)
        {
            return;
        }
        doorCU.nextPrompt();//advance prompt on pickup
        doorCU.proController.clearPrompters();//precaution against item disappearing
        //could have some kind of ienumerator that waits a second with a "wow you found a key" prompt
        
        inventory.addItem(this);
        //itemPickup.Play();
        Deactivate();//cannot destroy, causes scripting complications
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

    IEnumerator PlaySound()
    {
        itemPickup.Play();
        yield return new WaitWhile (()=> itemPickup.isPlaying);
        this.gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        StartCoroutine(PlaySound());
        //this.enabled = false;
    }
}
