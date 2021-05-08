using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HintTypes
{
    Consistant,//used for things that will always prompt, like the ladder (for now)
               //Must be first to be default
    Drop, //Dropping grabbables
    Pickup, //Picking up grabbables
    //uses pickup as the "end" enum for size comparison
    //add enums before and do not assign custom values
};

public class HintManager : MonoBehaviour
{

    private bool[] canPrompts;
    //must align with HintTypes
    private int[] promptsLeft =
    {
        0,
        2, //must be 2* pickup, due to bug w/ recalculating on pick up
        1

    };
    private string[] promptTexts = {
        "Press E to Drop",
        "Press E to Pick Up",
    };

    private void Start()
    {    
        canPrompts = new bool[(int)HintTypes.Pickup + 1];
        for(int i = 0; i<canPrompts.Length; i++)
        {
            canPrompts[i] = true;
        }
    }

    //Called by prompting functions to display a message
    //sets to desired text if possibe and delays when next prompt will be shown
    public void prompt(HintTypes hint, Text text, string prompt)
    {
        //Debug.Log("prompt type passed: " + hint);
        //if we're allowed to promt for that type
        if (canPrompts[(int)hint] || hint == HintTypes.Consistant)
        {
            text.text = prompt;
            //Decrement amount of times to remind
            promptsLeft[(int)hint]--;
            if (promptsLeft[(int)hint]<=0)
            {
                canPrompts[(int)hint] = false;
            }
        }        
    }
}
