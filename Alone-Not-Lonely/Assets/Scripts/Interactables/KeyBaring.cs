﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBaring : Interactable
{
    private bool containsKey = true;
    private PlayerInventory pIn;
    public int ID;
    private bool controlSwapThisFrame = false;
    private ContextualUI myOpen;
   
    public float keyPressCooldown = 1f, showDelayTime = .5f;
    private float currentKeyCooldown = 0f;
    private bool inKeyCooldown = false;
    private AudioSource boxSound;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerInventory.instance.puzzlePieces.Contains(this.ID))
        {
            Destroy(this.gameObject);
        }
        myOpen = GetComponent<ContextualUI>();
        base.playerRef = Player.instance;
        pIn = PlayerInventory.instance;
        if (objectAnimator != null)
        {
            boxSound = gameObject.GetComponent<AudioSource>();
        }
    }

    protected void keyGrab()
    {
        if (!playerRef.paused && !inKeyCooldown)
        {
            if (inRange && !open)
            {
                inKeyCooldown = true;
              
                open = true;
                if (objectAnimator!=null){ 
                    boxSound.Play();
                }
                StartCoroutine("viewPuzzlePiece");
            }
            else if (inRange && open)
            {
                inKeyCooldown = true;
                Debug.Log("Player is in range and the object is already open");
                /*if (objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", false);
                }*/

                open = false;
                if (containsKey)
                {
                    //set ID to that of this obj
                    //hiddenObj.gameObject.GetComponent<Item>().ID = this.ID;
                    pIn.addPuzzlePiece(this.ID);
                    pIn.addItem(hiddenObj.gameObject.GetComponent<Item>());
                }
                containsKey = false;
                PutDownKey();
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ForceDrop()
    {
        inKeyCooldown = true;

        open = false;
        PutDownKey();
    }

    private void Update()
    {
        if (controlSwapThisFrame)
        {
            Debug.Log("ControlSwap");
            controlSwapThisFrame = false;
            playerRef._actionMap.ViewingObject.InteractionTest.performed += interact => keyGrab();
            playerRef._actionMap.ViewingObject.RotateObj.performed += rot => base.Rotate(rot.ReadValue<Vector2>());
        }

        if(inKeyCooldown && currentKeyCooldown < keyPressCooldown)
        {
            currentKeyCooldown += Time.deltaTime;
        }
        else if(inKeyCooldown && currentKeyCooldown >= keyPressCooldown)
        {
            inKeyCooldown = false;
            currentKeyCooldown = 0f;
        }
    }

    IEnumerator viewPuzzlePiece()
    {
        if (objectAnimator != null)
        {
            objectAnimator.SetBool("OpenObj", true);
        }

        playerRef._actionMap.Platforming.Disable();
        yield return new WaitForSeconds(showDelayTime);
        playerRef._actionMap.ViewingObject.Enable();
        controlSwapThisFrame = true;
        hiddenObjInstance = Instantiate(hiddenObj, Camera.main.gameObject.transform.position + (Camera.main.gameObject.transform.forward * .75f), Quaternion.identity);

        //change prompt
        myOpen.nextPrompt();
    }

    void PutDownKey()
    {
        playerRef._actionMap.Platforming.Enable();
        playerRef._actionMap.ViewingObject.Disable();
        base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        playerRef._actionMap.ViewingObject.RotateObj.performed -= rot => Rotate(rot.ReadValue<Vector2>());
        Destroy(hiddenObjInstance);

        //change prompt
        myOpen.nextPrompt();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
            base.playerRef._actionMap.Platforming.InteractionTest.performed += interact => keyGrab();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exited Trigger");
            base.OnTriggerExit(other);
            if(open)
                ForceDrop();
            base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        }
    }
}
