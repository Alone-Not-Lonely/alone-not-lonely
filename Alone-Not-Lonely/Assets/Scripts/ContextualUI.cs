using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ContextualUI : MonoBehaviour
{
    //public bool conditionMet = false;//could be removed
    public string[] messages = new string[2];//default, can be overwritten
    public int[] maxUsages = new int[2];//times this should be prompted
    public int startPoint = 0, endPoint = 0;
    private int currInd = 0;//make private
    public promptType myPType; 
    [HideInInspector]
    public PromptController proController;//making public late change
    

    protected void Start()
    {
        //Get reference to conText object
        //set up persistence
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();
        //Debug.Log(this.name + " at Start() player count is " + objs.Length);
        proController = PromptController.instance;
    }

    //Called by object itself to progress the prompt counter
    public void nextPrompt()
    {
        proController.addToPrompters(this);//just in case prompter hasn't been triggered yet
        proController.incPromptUsages(myPType, currInd);
        currInd++;
        //loop to correct point
        if (currInd > endPoint)
        {
            //Debug.Log("going back to start point");
            currInd = startPoint;
        }
        proController.updatePrompt(this);//underthought, could cause problems later
    }

    public string getMessage()
    {
        return messages[currInd];
    }

    public int getCurrInd()
    {
        return currInd;
    }

    public bool canPrompt(int promptCount)
    {
        //Debug.Log("prompt count = " + promptCount  +", maxUsages = " + maxUsages[currInd]);
        return (promptCount <= maxUsages[currInd]);
    }

    public void setCID(int x)
    {
        currInd = x;
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            proController.addToPrompters(this);
            //Debug.Log("Staying in Collider");
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            proController.removeFromPrompters(this);
            //Debug.Log("Leaving Collider");
        }
    }

    private void OnDisable() {
        if(proController != null)
        {
            proController.clearPrompters();
        }
    }
}
