using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtticLevelEndController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator ballFallingAnim;
    public Text interactionUI;
    public Text cantInteractUI;

    private bool ableToInteract;
    private Collider interactionCollider;
    public AtticLadderController ladderController;

    public Player playerRef;

    private MeshRenderer meshRenderer;

    private AudioSource sfx;
    private void Awake() {
    }
    void Start()
    {
        playerRef = (Player)FindObjectOfType<Player>();
        Debug.Log("Player: " + playerRef);
        playerRef._actionMap.Platforming.Use.performed += grab => InteractAttempt();
        interactionUI.gameObject.SetActive(false);
        cantInteractUI.gameObject.SetActive(false);
        ableToInteract = false;
        interactionCollider = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        sfx = GetComponent<AudioSource>();
        //ladderController = (AtticLadderController)FindObjectOfType(typeof(AtticLadderController));
    }

    // Update is called once per frame
    void InteractAttempt()
    {
        if(ableToInteract && !ladderController.boxBlockingExit)
        {
            interactionCollider.enabled = false;
            ballFallingAnim.SetBool("PushBall", true);
            //fancy camera movements? VFX? all fair game
            //and then transition into next level (or transitional animation)
            interactionUI.gameObject.SetActive(false);
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
        if(other.CompareTag("Player"))
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

    void TriggerLadderFall()
    {
        sfx.Play();
        ladderController.AnimateOpenLadder();
        meshRenderer.enabled = false;
    }
}
