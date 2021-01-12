using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gamePaused;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }

        if(gamePaused)
        {
            UpdatePauseUI();
        }
    }

    void UpdatePauseUI()
    {
        
    }
}
