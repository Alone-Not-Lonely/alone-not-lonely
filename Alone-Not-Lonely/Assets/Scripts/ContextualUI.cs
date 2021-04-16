﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ContextualUI : MonoBehaviour
{
    public bool conditionMet = false;
    public Text contextInitial;
    public Text contextSecondary;
    public string messageInitial = "Initial message";
    public string messageSecondary = "Secondary message";

    private bool inRange = false;

    private void Start() {
        Text[] allText = (Text[])FindObjectsOfType(typeof(Text));
        foreach(Text t in allText)
        {
            if(t.gameObject.CompareTag("UIInit"))
            {
                contextInitial = t;
            }
            else if(t.gameObject.CompareTag("UISec"))
            {
                contextSecondary = t;
            }
        }
        contextInitial.text = "";
        contextSecondary.text = "";
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!conditionMet)
            {
                contextInitial.text = messageInitial;
                contextInitial.gameObject.SetActive(true);
            }
            else
            {
                contextSecondary.text = messageSecondary;
                contextSecondary.gameObject.SetActive(true);
            }
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            contextInitial.gameObject.SetActive(false);
            contextSecondary.gameObject.SetActive(false);
            contextInitial.text = "";
            contextSecondary.text = "";
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
            contextInitial.gameObject.SetActive(true);
            contextSecondary.gameObject.SetActive(false);
            contextInitial.text = messageInitial;
            contextSecondary.text = "";
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
            contextInitial.gameObject.SetActive(false);
            contextSecondary.gameObject.SetActive(true);
            contextInitial.text = "";
            contextSecondary.text = messageSecondary;
        }
        else
        {
            conditionMet = true;
        }
    }
}
