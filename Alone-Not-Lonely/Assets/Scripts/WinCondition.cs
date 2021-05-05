using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public void onWin()
    {
        ProgressionTracker[] p = FindObjectsOfType<ProgressionTracker>();
        p[0].MarkSceneCompleted("Attic2");
    }
}
