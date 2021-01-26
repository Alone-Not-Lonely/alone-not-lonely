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

    private bool ableToInteract;
    private Collider interactionCollider;

    void Start()
    {
        interactionUI.gameObject.SetActive(false);
        ableToInteract = false;
        interactionCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ableToInteract && Input.GetKeyDown(KeyCode.E))
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
            interactionUI.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            ableToInteract = false;
            interactionUI.gameObject.SetActive(false);
        }
    }
}
