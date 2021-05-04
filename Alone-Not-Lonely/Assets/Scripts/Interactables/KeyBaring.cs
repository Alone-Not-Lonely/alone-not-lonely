using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBaring : Interactable
{
    private bool containsKey = true;
    private PlayerInventory pIn;
    public int ID;
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

    void viewPuzzlePiece()
    {
        playerRef._actionMap.Platforming.Disable();
        playerRef._actionMap.ViewingObject.Enable();
        playerRef._actionMap.ViewingObject.InteractionTest.performed += interact => keyGrab();
        playerRef._actionMap.ViewingObject.RotateObj.performed += rot => base.Rotate(rot.ReadValue<Vector2>());
        hiddenObjInstance = Instantiate(hiddenObj, Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward, Quaternion.identity);
    }

    void PutDownKey()
    {
        playerRef._actionMap.Platforming.Enable();
        playerRef._actionMap.ViewingObject.Disable();
        base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        playerRef._actionMap.ViewingObject.RotateObj.performed -= rot => Rotate(rot.ReadValue<Vector2>());
        Destroy(hiddenObjInstance);
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
        if(other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);
            base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        }
    }

}
