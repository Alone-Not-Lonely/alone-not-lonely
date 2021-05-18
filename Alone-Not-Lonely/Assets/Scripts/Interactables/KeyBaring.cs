using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBaring : Interactable
{
    private bool containsKey = true;
    private PlayerInventory pIn;
    public int ID;
    private bool controlSwapThisFrame = false;
    // Start is called before the first frame update
    void Start()
    {
        base.playerRef = (Player)FindObjectOfType(typeof(Player));
        pIn = FindObjectOfType<PlayerInventory>();
    }

    protected void keyGrab()
    {
        if (!playerRef.paused && containsKey)
        {
            if (inRange && !open)
            {
                Debug.Log("Opening Object ");
                //openText.gameObject.SetActive(false);
                //closeText.gameObject.SetActive(true);
                if (objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", true);
                }
                open = true;
                //base.LookAtObject();
                viewPuzzlePiece();
            }
            else if (inRange && open)
            {
                //openText.gameObject.SetActive(false);
                //closeText.gameObject.SetActive(false);
                if (objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", false);
                }
                
                open = false;
                if (containsKey) { 
                    //set ID to that of this obj
                    //hiddenObj.gameObject.GetComponent<Item>().ID = this.ID;
                    pIn.addPuzzlePiece(this.ID);
                    pIn.addItem(hiddenObj.gameObject.GetComponent<Item>());
                }
                containsKey = false;
                PutDownKey();
                this.gameObject.SetActive(false);
                this.GetComponent<OpenableUI>().contextInitial.text = "";
                this.GetComponent<OpenableUI>().contextSecondary.text = "";
            }
        }
    }

    private void Update() {
        if(controlSwapThisFrame)
        {
            controlSwapThisFrame = false;
            playerRef._actionMap.ViewingObject.InteractionTest.performed += interact => keyGrab();
            playerRef._actionMap.ViewingObject.RotateObj.performed += rot => base.Rotate(rot.ReadValue<Vector2>());
        }    
    }

    void viewPuzzlePiece()
    {
        playerRef._actionMap.Platforming.Disable();
        playerRef._actionMap.ViewingObject.Enable();
        controlSwapThisFrame = true;
        hiddenObjInstance = Instantiate(hiddenObj, Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward, Quaternion.identity);
        Debug.Log("Object Instantiated " + hiddenObjInstance);
    }

    void PutDownKey()
    {
        playerRef._actionMap.Platforming.Enable();
        playerRef._actionMap.ViewingObject.Disable();
        base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        playerRef._actionMap.ViewingObject.RotateObj.performed -= rot => Rotate(rot.ReadValue<Vector2>());
        Destroy(hiddenObjInstance);
        Debug.Log("Object Destroyed ");
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
            base.playerRef._actionMap.Platforming.InteractionTest.performed += interact => keyGrab();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger");
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Exited Trigger");
            base.OnTriggerExit(other);
            base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        }
    }

}
