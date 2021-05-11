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
    private string[] prompts = //must align with promptType List
    {
        "Press 'e' to pick up key",
        "Press 'e' to open box",
        "Puzzle piece saved! press 'e' to close box",
        "Press 'e' to move",
        "Press 'e' to put down"
    };

    private int[] uses =
    {
        1,
        1,
        1,
        1,
        1
    };

    public void setPrompt(ContextualUI cui)
    {
        int i = (int)cui.currPromptType;
        if (uses[i] > 0)
        {
            Debug.Log("PromptController: changing text");
            cui.currentMessage = prompts[i];
            //add decrementing counter;
        }
        else
        {
            cui.currentMessage = "";
        }
    }

}
