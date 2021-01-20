using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gamePaused;

    public GameObject pausePrefab;

    private Player playerRef;
    private AudioHighPassFilter highPassFilter;
    void Start()
    {
        pausePrefab.SetActive(false);
        //make audio reverb-y in the pause menu
        highPassFilter = GetComponent<AudioHighPassFilter>();
        highPassFilter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            Unpause();
        }

        if(gamePaused)
        {
            UpdatePauseUI();
        }
    }

    void UpdatePauseUI()
    {

    }

    public bool isPaused()
    {
        return gamePaused;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gamePaused = true;
        pausePrefab.SetActive(true);
        highPassFilter.enabled = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        gamePaused = false;
        pausePrefab.SetActive(false);
        highPassFilter.enabled = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Debug.Log("Application closed");
        Application.Quit();
    }
}
