﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodModule : MonoBehaviour
{
    public AudioClip thisClip;
    AudioClip GetClip()
    {
        return thisClip;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            MoodModuleController mController = other.GetComponent<MoodModuleController>();
            mController.ChangeSong(thisClip);
        }
    }
}
