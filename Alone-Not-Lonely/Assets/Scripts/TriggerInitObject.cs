using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInitObject : MonoBehaviour
{
    public GameObject objectToInit;
    private AudioSource aS;
    public bool nonPlayerTrigger = false;

    private AudioSource gabeAs;
    private void Start()
    {
        aS = objectToInit.GetComponent<AudioSource>();
        gabeAs = GetComponent<AudioSource>();
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(!nonPlayerTrigger && other.CompareTag("Player"))
        {
            aS.Play(0);
            objectToInit.SetActive(true);
            objectToInit.GetComponent<Animator>().SetBool("StartAnim", true);
            foreach(Transform child in objectToInit.transform)
            {
                child.gameObject.GetComponent<Animator>().SetBool("StartAnim", true);
            }
            Deactivate();
        }
        else if(nonPlayerTrigger && other.CompareTag("Grabable"))
        {
            aS.Play(0);
            objectToInit.GetComponent<Animator>().SetBool("StartAnim", true);
            foreach(Transform child in objectToInit.transform)
            {
                child.gameObject.GetComponent<Animator>().SetBool("StartAnim", true);
            }
            //gabeAs.Play();
            Deactivate();
        }
    }

    IEnumerator PlaySound()
    {
        if(gabeAs)
        {
            gabeAs.Play();
            yield return new WaitWhile (()=> gabeAs.isPlaying);
            this.enabled = false;
        }
        else
        {
            this.enabled = false;
            yield return null;
        }
    }

    public void Deactivate()
    {
        StartCoroutine(PlaySound());
        //this.enabled = false;
    }
}
