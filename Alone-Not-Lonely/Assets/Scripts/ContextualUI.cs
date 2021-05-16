﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ContextualUI : MonoBehaviour
{
    public bool conditionMet = false;//could be removed
    public Text conText;
    //public promptType currType; //this will change based on type of prompt
    public string currentMessage = ""; //this will change what is actually printed
    private PromptController proController;
    private bool inRange = false;//could be removed

    protected void OnDisable()
    {
        //conText.text = "";
        //currentMessage = "";
    }

    protected void Start() {
        //Get reference to conText object
        Text[] allText = (Text[])FindObjectsOfType(typeof(Text), true);
        foreach(Text t in allText)
        {
            if(t.gameObject.CompareTag("UIInit"))
            {
                conText = t;
            }
        }
        //set up persistence
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();
        //Debug.Log(this.name + " at Start() player count is " + objs.Length);

        conText.gameObject.SetActive(true);
        //conText.text = "";
        //currentMessage = "";
        proController = FindObjectOfType<PromptController>();
    }

    //basically just a wrapper to ease relations between objects and
    //prompt controller
    public void updatePrompt(string prompt)
    {
        conText.text = proController.setPrompt(prompt);
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
            */
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

    /*
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
