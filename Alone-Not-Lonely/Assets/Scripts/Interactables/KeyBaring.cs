using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBaring : Interactable
{
    private bool containsKey = true;
    private PlayerInventory pIn;
    public int ID;
    private bool controlSwapThisFrame = false;
    private ContextualUI myOpen;

    public float keyPressCooldown = 1f;
    private float currentKeyCooldown = 0f;
    private bool inKeyCooldown = false;
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
    }

    protected void keyGrab()
    {
        Debug.Log("F pressed, grab attempted");
        if (!playerRef.paused && !inKeyCooldown)
        {
            Debug.Log("Player is not paused");
            if (inRange && !open)
            {
                inKeyCooldown = true;
                Debug.Log("Player is in range and the object is not open yet");
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
                inKeyCooldown = true;
                Debug.Log("Player is in range and the object is already open");
                //openText.gameObject.SetActive(false);
                //closeText.gameObject.SetActive(false);
                if (objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", false);
                }

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
                //this.GetComponent<OpenableUI>().conText.text = "";
                //this.GetComponent<ContextualUI>().conText.text = "";//TESTING PURPOSES
            }
        }
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

    void viewPuzzlePiece()
    {
        playerRef._actionMap.Platforming.Disable();
        playerRef._actionMap.ViewingObject.Enable();
        controlSwapThisFrame = true;
        hiddenObjInstance = Instantiate(hiddenObj, Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward, Quaternion.identity);

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
            base.playerRef._actionMap.Platforming.InteractionTest.performed -= interact => keyGrab();
        }
    }
}
