﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame(string scenenamepass)
    {
        SceneManager.LoadScene(scenenamepass);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneName);
    }
}
