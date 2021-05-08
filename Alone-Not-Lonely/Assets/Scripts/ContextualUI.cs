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
    //public Text contextInitial;
    //public Text contextSecondary;

    public string messageInitial = "Initial message";
    public string messageSecondary = "Secondary message";

    private bool inRange = false;

    public HintTypes p1Type;
    public HintTypes p2Type;
    private HintManager hMan;


    protected void OnDisable() {
        if(conText != null)
        {
            //contextInitial.gameObject.SetActive(false);
            //contextInitial.text = "";
            conText.gameObject.SetActive(false);
            conText.text = "";
        }
        /*
        if(contextInitial != null)
        {
            contextInitial.gameObject.SetActive(false);
            contextInitial.text = "";
        }
        
        if(contextSecondary != null)
        {
            contextSecondary.gameObject.SetActive(false);
            contextSecondary.text = "";
        }
        */
    }

    protected void Start() {
        Text[] allText = (Text[])FindObjectsOfType(typeof(Text), true);
        foreach(Text t in allText)
        {
            if(t.gameObject.CompareTag("UIInit"))
            {
                conText = t;
                //contextInitial = t;
            }/*
            else if(t.gameObject.CompareTag("UISec"))
            {
                contextSecondary = t;
            }*/
        }
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();
        //Debug.Log(this.name + " at Start() player count is " + objs.Length);
        //contextInitial.text = "";
        //contextSecondary.text = "";
        conText.text = "";
        hMan = FindObjectOfType<HintManager>();
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
            conText.gameObject.SetActive(true);
            if (!conditionMet)
            {
                //contextInitial.text = messageInitial;
                hMan.prompt(p1Type, conText, messageInitial);
                //contextInitial.gameObject.SetActive(true);
            }
            else
            {
                //contextSecondary.text = messageSecondary;
                hMan.prompt(p2Type, conText, messageSecondary);
                //contextSecondary.gameObject.SetActive(true);
            }
            inRange = true;
        }
    }

    protected void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            /*
            contextInitial.gameObject.SetActive(false);
            contextSecondary.gameObject.SetActive(false);
            contextInitial.text = "";
            contextSecondary.text = "";
            */
            conText.gameObject.SetActive(false);
            conText.text = "";
        }
        inRange = false;
    }

    public void SetConditionMet(bool setting)
    {
        conditionMet = setting;
    }

    public void ChangeToContextInit()
    {
        //Debug.Log("Changed to Init");
        if (inRange)
        {
            //contextInitial.gameObject.SetActive(true);
            //contextSecondary.gameObject.SetActive(false);
            //contextInitial.text = messageInitial;
        hMan.prompt(p1Type, conText, messageInitial);
        //contextSecondary.text = "";
        }
        //else
        //{
        conditionMet = false;
        //}
    }

    public void ChangeToContextSecondary()
    {
        if(inRange)
        {
            //contextInitial.gameObject.SetActive(false);
            //contextSecondary.gameObject.SetActive(true);
            //contextInitial.text = "";
            //contextSecondary.text = messageSecondary;
            hMan.prompt(p2Type, conText, messageSecondary);
        }
        //else
        //{
        conditionMet = true;
        //}
    }
}
