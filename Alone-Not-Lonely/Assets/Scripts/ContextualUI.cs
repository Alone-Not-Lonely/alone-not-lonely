using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ContextualUI : MonoBehaviour
{
    public bool conditionMet = false;
    public Text conText;
    //public string[] messages;
    //public string messageInitial = "Initial message";
    //public string messageSecondary = "Secondary message";
    public promptType currPromptType; //this will change based on type of prompt
    public string currentMessage = ""; //this will change what is actually printed
    private PromptController proController;
    private bool inRange = false;

    protected void OnDisable()
    {
        conText.text = "";
        currentMessage = "";
    }

    protected void Start() {
        Text[] allText = (Text[])FindObjectsOfType(typeof(Text), true);
        foreach(Text t in allText)
        {
            if(t.gameObject.CompareTag("UIInit"))
            {
                conText = t;
            }
        }
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();
        //Debug.Log(this.name + " at Start() player count is " + objs.Length);
        conText.text = "";
        currentMessage = "";
        proController = FindObjectOfType<PromptController>();
    }

    protected void OnEnable() {
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
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
            */
            proController.setPrompt(currPromptType, currentMessage);
        }
    }

    protected void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            //conText.gameObject.SetActive(false);
            //contextSecondary.gameObject.SetActive(false);
            conText.text = "";
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
}
