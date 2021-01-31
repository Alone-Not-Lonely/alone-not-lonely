using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AtticLevelEndController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator ballFallingAnim;
    public Text interactionUI;
    public Text cantInteractUI;

    private bool ableToInteract;
    private Collider interactionCollider;
    private AtticLadderController ladderController;

    private void Awake() {
        Player playerRef = (Player)FindObjectOfType(typeof(Player));
        playerRef._actionMap.Platforming.Use.performed += grab => InteractAttempt();
    }
    void Start()
    {
        interactionUI.gameObject.SetActive(false);
        cantInteractUI.gameObject.SetActive(false);
        ableToInteract = false;
        interactionCollider = GetComponent<SphereCollider>();
        ladderController = (AtticLadderController)FindObjectOfType(typeof(AtticLadderController));
    }

    // Update is called once per frame
    void InteractAttempt()
    {
        if(ableToInteract && !ladderController.boxBlockingExit)
        {
            interactionCollider.enabled = false;
            //trigger animation
            //fancy camera movements? VFX? all fair game
            //and then transition into next level (or transitional animation)
            interactionUI.gameObject.SetActive(false);
            SceneManager.LoadScene("EndPlaytest");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ableToInteract = true;
            if(!ladderController.boxBlockingExit)
            {
                interactionUI.gameObject.SetActive(true);
            }
            else
            {
                cantInteractUI.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(ladderController.boxBlockingExit)
        {
            interactionUI.gameObject.SetActive(false);
            cantInteractUI.gameObject.SetActive(true);
        }
        else
        {
            interactionUI.gameObject.SetActive(true);
            cantInteractUI.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            ableToInteract = false;
            if(interactionUI.gameObject.activeInHierarchy)
            {
                interactionUI.gameObject.SetActive(false);
            }
            else
            {
                cantInteractUI.gameObject.SetActive(false);
            }
        }
    }
}
