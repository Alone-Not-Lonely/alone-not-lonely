using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(KeyBaring))]
public class OpenableUI : ContextualUI
{
    KeyBaring openable;

    void Start()
    {
        base.Start();
        openable = GetComponent<KeyBaring>();
        //conText.text = "";
    }

    void OnEnable() {
        //base.OnEnable();
    }

    void OnDisable()
    {
        base.OnDisable();
    }

    void Update()
    {

    }

    
}
