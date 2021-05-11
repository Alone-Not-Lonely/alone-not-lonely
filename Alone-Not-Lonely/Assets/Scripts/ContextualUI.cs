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
            conText.gameObject.SetActive(false);
            conText.text = "";
        }
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
                hMan.prompt(p1Type, conText, messageInitial);
            }
            else
            {
                hMan.prompt(p2Type, conText, messageSecondary);
            }
            inRange = true;
        }
    }

    protected void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
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
        if (inRange)
        {
        hMan.prompt(p1Type, conText, messageInitial);
        }
        conditionMet = false;
    }

    public void ChangeToContextSecondary()
    {
        if(inRange)
        {
            hMan.prompt(p2Type, conText, messageSecondary);
        }
        conditionMet = true;
    }
}
