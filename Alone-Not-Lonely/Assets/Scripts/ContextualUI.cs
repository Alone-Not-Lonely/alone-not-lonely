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
    public int loopPoint = 0;
    private int currInd = 0;//make private
    public promptType myPType; 
    //public string currentMessage = ""; //this will change what is actually printed
    private PromptController proController;
    //private bool inRange = false;//could be removed


    protected void Start()
    {
        //Get reference to conText object
        //set up persistence
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();
        //Debug.Log(this.name + " at Start() player count is " + objs.Length);
        proController = FindObjectOfType<PromptController>();
    }


    protected void OnDisable()
    {

    }

    //Called by object itself to progress the prompt counter
    public void nextPrompt()
    {
        proController.incPromptUsages(myPType, currInd);
        currInd++;
        //loop to correct point
        if (currInd >= messages.Length)
        {
            currInd = loopPoint;
        }
        proController.updatePrompt();//underthought, could cause problems later
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

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                proController.addToPrompters(this);

        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            proController.removeFromPrompters(this);
        }
    }
}
