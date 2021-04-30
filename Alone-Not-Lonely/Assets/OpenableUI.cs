using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(KeyBaring))]
public class OpenableUI : ContextualUI
{
    KeyBaring openable;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        openable = GetComponent<KeyBaring>();
        contextInitial.text = "";
        contextSecondary.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(openable.open == true)
        {
            ChangeToContextSecondary();
        }
        else
        {
            ChangeToContextInit();
        }
    }

    
}
