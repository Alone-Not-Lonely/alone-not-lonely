using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum promptType {
    keyPickUp,
    openableOpen,
    openableClose,
    movableGrab,
    movablePutDown
};
public class PromptController : MonoBehaviour
{
    
    public Dictionary<string, int> prompts;
    private void Start()
    {
        prompts = new Dictionary<string, int>();
        //All prompts must first be added into this dictionary
        prompts.Add("Press 'F' to open box", 1);
        prompts.Add("Puzzle piece saved! press 'F' to close box", 1);
        prompts.Add("Press 'F' to pick up key", 1);
        prompts.Add("Press 'e' to pick up", 1);
        prompts.Add("Press 'e' to put down", 1);
    }

    //Called every time a player enters the vicinity 
    //of a prompting object
    //A script will request it's attachted context ui change its prompt
    //and this will determine if, globally, we haven't heard enough of it already
    //public void setPrompt(ContextualUI cui)
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

}
