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

    public void setPrompt(promptType type, string promptText)
    {
        int i = (int)type;
        if (uses[i] > 0)
        {
            promptText = prompts[i];
            //add decrementing counter;
        }
        else
        {
            promptText = "";
        }
    }

}
