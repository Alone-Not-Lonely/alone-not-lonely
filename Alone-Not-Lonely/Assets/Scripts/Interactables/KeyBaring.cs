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
        base.playerRef._actionMap.Platforming.InteractionTest.performed += interact => keyGrab();
        pIn = FindObjectOfType<PlayerInventory>();
    }

    protected void keyGrab()
    {
        if (!playerRef.paused)
        {
            if (inRange && !open)
            {
                openText.gameObject.SetActive(false);
                closeText.gameObject.SetActive(true);
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
                openText.gameObject.SetActive(false);
                closeText.gameObject.SetActive(false);
                if (objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", false);
                }
                
                open = false;
                if (containsKey) { 
                    //set ID to that of this obj
                    hiddenObj.gameObject.GetComponent<Item>().ID = this.ID;
                    pIn.addItem(hiddenObj.gameObject.GetComponent<Item>());
                }
                hiddenObj = new GameObject();
                hiddenObj.AddComponent<Transform>();
                containsKey = false;
                base.PutDownObject();
                this.GetComponent<SphereCollider>().enabled = false;
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

}
