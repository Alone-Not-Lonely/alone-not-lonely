using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private ImaginaryEntity[] entities;
    // Start is called before the first frame update
    void Start()
    {
        entities = FindObjectsOfType<ImaginaryEntity>();
    }

    public void onWin()
    {
        entities = FindObjectsOfType<ImaginaryEntity>();
        foreach(ImaginaryEntity entity in entities)
        {
            entity.gannonVoiceDie();
        }
    }
}
