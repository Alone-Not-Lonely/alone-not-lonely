using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInitObject : MonoBehaviour
{
    public GameObject objectToInit;
    private AudioSource aS;
    private void Start()
    {
        aS = objectToInit.GetComponent<AudioSource>();
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            aS.Play(0);
            objectToInit.GetComponent<Animator>().SetBool("StartAnim", true);
            foreach(Transform child in objectToInit.transform)
            {
                child.gameObject.GetComponent<Animator>().SetBool("StartAnim", true);
            }
            Deactivate();
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
