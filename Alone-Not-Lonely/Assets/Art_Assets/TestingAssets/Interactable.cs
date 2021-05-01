using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    protected bool inRange = false;
    public bool open = false;
    //public Text openText;
    //public Text closeText;

    protected Player playerRef;

    public Animator objectAnimator;
    public GameObject hiddenObj;

    protected GameObject hiddenObjInstance;

    void Awake() 
    {

    }
    void Start()
    {
        //openText.gameObject.SetActive(false);
        //closeText.gameObject.SetActive(false);
    }

    protected void PlayerInteract()
    {
        if (!playerRef.paused)
        {
            if (inRange && !open)
            {
                //openText.gameObject.SetActive(false);
                //closeText.gameObject.SetActive(true);
                if(objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", true);
                }
                open = true;
                LookAtObject();
            }
            else if(inRange && open)
            {
                //openText.gameObject.SetActive(true);
                //closeText.gameObject.SetActive(false);
                if(objectAnimator != null)
                {
                    objectAnimator.SetBool("OpenObj", false);
                }
                open = false;
                PutDownObject();
            }
        }
    }

    protected void LookAtObject()
    {
        playerRef._actionMap.Platforming.Disable();
        playerRef._actionMap.ViewingObject.Enable();
        playerRef._actionMap.ViewingObject.InteractionTest.performed += interact => PlayerInteract();
        playerRef._actionMap.ViewingObject.RotateObj.performed += rot => Rotate(rot.ReadValue<Vector2>());
        hiddenObjInstance = Instantiate(hiddenObj, Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward, Quaternion.identity);
    }

    protected void Rotate(Vector2 inVec)
    {
        if(hiddenObjInstance != null)
        {
            hiddenObjInstance.transform.Rotate(inVec.x/10, 0, inVec.y/10);
        }
    }

    protected void PutDownObject()
    {
        playerRef._actionMap.Platforming.Enable();
        playerRef._actionMap.ViewingObject.Disable();
        playerRef._actionMap.Platforming.InteractionTest.performed += interact => PlayerInteract();
        playerRef._actionMap.ViewingObject.RotateObj.performed -= rot => Rotate(rot.ReadValue<Vector2>());
        Destroy(hiddenObjInstance);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
            //openText.gameObject.SetActive(true);
            //closeText.gameObject.SetActive(false);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = false;
            //openText.gameObject.SetActive(false);
            //closeText.gameObject.SetActive(false);
        }
    }
}
