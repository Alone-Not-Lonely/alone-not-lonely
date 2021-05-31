using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum promptType
{
    keyBaring,
    grabbable,
    gameplayPuzzle,
    prevLevel,
    door,
    key
};
//Singleton in charge of managing the prompting of player.
//ContextualUI objects make requests to this object
public class PromptController : MonoBehaviour
{
    private List<ContextualUI> prompters;
    private ContextualUI currPrompter, prevPrompter;
    public Text conText;
    private PlayerAbilityController pAbil;
    public Dictionary<promptType, int[]> prompts;
    private bool proJustAdded = false;

    public static PromptController instance;

    private void Awake() {
        PromptController[] objs = (PromptController[])FindObjectsOfType<PromptController>();

        if (objs.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            //DontDestroyOnLoad(this.gameObject); //already done in parent
            instance = this;
        }
    }
    
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

        //prompts will recieve the prompts and number of times the prompt has been used
        prompts = new Dictionary<promptType, int[]>();
    }

    public void clearText()
    {
        conText.text = "";
    }

    //used in case of death of player
    public void clearPrompters()
    {
        conText.text = "";
        prompters.Clear();
    }

    //Used in level transitions to remove level-specific prompters
    //(i.e. the ladder from the attic)
    public void clearSpecificPrompter(ContextualUI pmtr)
    {
        prompters.Remove(pmtr);
    }

    //Puts a prompter on the list of possible prompters
    public void addToPrompters(ContextualUI prompter)
    {
        if (!prompters.Contains(prompter))
        {
            //Debug.Log("Adding: " + prompter.gameObject.name);
            prompters.Add(prompter);
        }

        //sets up space for recording the usages of certain prompts.
        //MUST BE CAREFUL ABOUT # OF SLOTS/P-TYPE
        if (!prompts.ContainsKey(prompter.myPType))
        {
            prompts[prompter.myPType] = new int[prompter.maxUsages.Length];
        }
        //Debug.Log("Prompts dictionary size: " + prompts.Count);
    }

    //Removes prompter from list of possible prompters
    public void removeFromPrompters(ContextualUI prompter)
    {
        //Debug.Log("Removing: " + prompter.gameObject.name);
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
            updatePrompt(currPrompter);
        }
        else if (currPrompter == null)
        {
            //Debug.Log("Clearing Text");
            conText.text = "";
        }
        prevPrompter = currPrompter;//the new becomes the old
        //prompters.Clear();//clear current registry (inefficient?)
    }

    public void updatePrompt(ContextualUI caller)
    {
        if (currPrompter == caller)
        {
            conText.text = currPrompter.getMessage();
            //Debug.Log(conText.text);
        }
    }

    //used by external functions
    //increments the times a particular prompt was used
    public void incPromptUsages(promptType prompt, int currInd)
    {
        prompts[prompt][currInd]++;
        //Debug.Log("Prompt type: " + currInd + " has been called " + prompts[prompt][currInd] + " times");
    }

    //determines if a prompter can display its current prompt


    //recieves a variable number of prompters and returns the one that is nearest the player
    //TODO: Check if can even prompt anymore
    public ContextualUI pickPrompter()
    {
        float maxSim = -1;
        float tempSim;
        ContextualUI tempCon = null;
        foreach (ContextualUI prompter in prompters)
        {
            //Pass over a prompter that wouldn't print anything anyway
            //recall that the prompter's index is set by the prompter, not this script
            if (!prompter.canPrompt(prompts[prompter.myPType][prompter.getCurrInd()]))
            {
                //Debug.Log("Passing over promtper due to overused prompt");
                continue;
            }

            Vector3 dir = prompter.transform.position - transform.position;
            tempSim = Vector3.Dot(transform.forward, dir);

            //Debug.DrawRay(prompter.transform.position, dir);
            //Looking near check used to go here, not needed as look check now done externally
            if (tempSim > maxSim)
            {
                maxSim = tempSim;
                tempCon = prompter;
            }
        }

        return tempCon;
    }


}