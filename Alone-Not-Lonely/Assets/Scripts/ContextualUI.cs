using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ContextualUI : MonoBehaviour
{
    //public bool conditionMet = false;//could be removed
    public string[] messages;
    public int[] usages;
    public int loopPoint = 0;
    private int currInd = 0;//make private
    //public promptType currType; //this will change based on type of prompt
    //public string currentMessage = ""; //this will change what is actually printed
    private PromptController proController;
    //private bool inRange = false;//could be removed

    protected void OnDisable()
    {
        //conText.text = "";
        //currentMessage = "";
    }

    //Called by object itself to progress the prompt counter
    public void nextPrompt()
    {
        currInd++;
        usages[currInd]--;//this prompt has been used
        if (currInd >= messages.Length)
        {
            currInd = loopPoint;
        }
    }

    public string getMessage()
    {
        return messages[currInd];
    }

    protected void Start() {
        //Get reference to conText object
        //set up persistence
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();
        //Debug.Log(this.name + " at Start() player count is " + objs.Length);
        proController = FindObjectOfType<PromptController>();
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

    /*
        basically just a wrapper to ease relations between objects and
        prompt controller
        public void updatePrompt(string prompt)
        {
            conText.text = proController.setPrompt(prompt);
        }

        protected void OnEnable() {

        }
        */

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    /*
    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
            conText.text = proController.setPrompt(currentMessage);

            /*
            if(!conditionMet)
            {
                //conText.text = messageInitial;
                //conText.gameObject.SetActive(true);
            }
            else
            {
                //conText.text = messageSecondary;
                //contextSecondary.text = messageSecondary;
                //contextSecondary.gameObject.SetActive(true);
            }
            inRange = true;

            //sets prompt to be correct message
        }
    }

    protected void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            //conText.gameObject.SetActive(false);
            //contextSecondary.gameObject.SetActive(false);
            conText.text = "";//resets text
            //contextSecondary.text = "";
        }
        inRange = false;
    }

    public void SetConditionMet(bool setting)
    {
        conditionMet = setting;
    }


    public void ChangeToContextInit()
    {
        if(inRange)
        {
            //conText.gameObject.SetActive(true);
            //contextSecondary.gameObject.SetActive(false);
            //conText.text = messageInitial;
            //contextSecondary.text = "";
        }
        else
        {
            conditionMet = false;
        }
    }

    public void ChangeToContextSecondary()
    {
        if(inRange)
        {
            //conText.gameObject.SetActive(false);
            //contextSecondary.gameObject.SetActive(true);
            conText.text = "";
            //contextSecondary.text = messageSecondary;
        }
        else
        {
            conditionMet = true;
        }
    }
    */
}
