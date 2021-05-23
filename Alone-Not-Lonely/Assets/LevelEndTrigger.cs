using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject levelParent;
    
    private void Awake() {

    }

    private void Start() {
        if(ProgressionTracker.instance.alreadyVisited.Contains(levelParent.name))
        {
            CheckObjects(levelParent);
        }
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(levelParent.name != "Attic2" && other.CompareTag("Player"))
        {
            CheckObjects(levelParent);
        }
        else if(levelParent.name == "Attic2" && other.CompareTag("Grabable"))//check when chest is on door
        {
            CheckObjects(levelParent);
        }
    }

    void CheckObjects(GameObject curr)
    {
        ProgressionTracker.instance.MarkSceneCompleted(levelParent.name);
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
