using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum promptType
{
    keyPickUp,
    openableOpen,
    openableClose,
    movableGrab,
    movablePutDown
};
//Singleton in charge of managing the prompting of player.
//ContextualUI objects make requests to this object
public class PromptController : MonoBehaviour
{
    [SerializeField]
    private List<ContextualUI> prompters;
    private ContextualUI currPrompter, prevPrompter;
    public Text conText;
    private PlayerAbilityController pAbil;
    public Dictionary<string, int> prompts;
    private bool proJustAdded = false;
    private void Start()
    {
        prompters = new List<ContextualUI>();
        currPrompter = null;
        prevPrompter = null;//remembers what the prompter was last frame
        //getting conText
        Text[] allText = (Text[])FindObjectsOfType(typeof(Text), true);
        foreach (Text t in allText)
        {
            if (t.gameObject.CompareTag("UIInit"))
            {
                conText = t;
            }
        }

        conText.gameObject.SetActive(true);
        pAbil = FindObjectOfType<PlayerAbilityController>();

        prompts = new Dictionary<string, int>();
        //All prompts must first be added into this dictionary
        prompts.Add("Press 'F' to open box", 1);
        prompts.Add("Puzzle piece saved! press 'F' to close box", 1);
        prompts.Add("Press 'F' to pick up key", 1);
        prompts.Add("Press 'e' to pick up", 1);
        prompts.Add("Press 'e' to put down", 1);


    }

    //Puts a prompter on the list of possible prompters
    public void addToPrompters(ContextualUI prompter)
    {
        if (!prompters.Contains(prompter))
        {
            Debug.Log("Adding: " + prompter.gameObject.name);
            prompters.Add(prompter);
        }
    }

    //Removes prompter from list of possible prompters
    public void removeFromPrompters(ContextualUI prompter)
    {
        Debug.Log("Removing: " + prompter.gameObject.name);
        prompters.Remove(prompter);
        
    }

    //Called late to ensure triggers get all information settled
    public void LateUpdate()
    {
        currPrompter = pickPrompter();//TRYING STUFF OUT
        //Debug.Log(currPrompter.gameObject.name);
        //Get closest viable prompter
        if (currPrompter != null && currPrompter != prevPrompter)
        {
            conText.text = currPrompter.getMessage();   
        }
        else if(currPrompter == null)
        {
            //Debug.Log("Clearing Text");
            conText.text = "";
        }
        prevPrompter = currPrompter;//the new becomes the old
        //prompters.Clear();//clear current registry (inefficient?)
    }

    //recieves a variable number of prompters and returns the one that is nearest the player
    //TODO: Check if can even prompt anymore
    public ContextualUI pickPrompter()
    { 
        float maxSim = -1;
        float tempSim;
        ContextualUI tempCon = null;
        foreach (ContextualUI prompter in prompters)
        {
            Vector3 dir = prompter.transform.position - transform.position;
            tempSim = Vector3.Dot(transform.forward, dir);

                Debug.DrawRay(prompter.transform.position, dir);
            //Looking near check used to go here, not needed as look check now done externally
            if (tempSim > maxSim)
            {
                maxSim = tempSim;
                tempCon = prompter;
            }
        }

        return tempCon;
    }

    //Called every time a player enters the vicinity 
    //of a prompting object
    //A script will request it's attachted context ui change its prompt
    //and this will determine if, globally, we haven't heard enough of it already
    /*
    public string setPrompt(string nuPrompt)
    {
        //int i = (int)cui.currType;
        //check to see if we have more uses
        if (prompts[nuPrompt] > 0)
        {
            Debug.Log("PromptController: changing text");
            return nuPrompt;
            //add decrementing counter;
        }
        else
        {
            return "";
        }
    }
    */
}
