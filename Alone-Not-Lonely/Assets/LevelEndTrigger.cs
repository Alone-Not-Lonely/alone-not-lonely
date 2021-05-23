using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject levelParent;
    
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CheckObjects(levelParent);
        }
    }

    void CheckObjects(GameObject curr)
    {
        foreach(Transform child in curr.transform)
        {
            if(child.gameObject.CompareTag("Monster") ||
               child.gameObject.GetComponent<PatrolPointsController>()!=null ||
               child.gameObject.GetComponent<PortalController>() != null ||
               child.gameObject.GetComponent<ElevatorMonsterController>() != null ||
               child.gameObject.CompareTag("RemoveOnStart") ||
               child.gameObject.CompareTag("Deadly"))
            {
                child.gameObject.SetActive(false);
            }
            //else if(child.gameObject.GetComponent<Item>() != null && 
               //child.gameObject.GetComponent<Item>().key.Contains("Key"))
            //{
                //child.gameObject.SetActive(false);
            //}
            CheckObjects(child.gameObject);
        }
    }
}
