using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gamePaused;

    public GameObject pausePrefab;

    private Player playerRef;
    public AudioMixer mixer;
    public AudioMixerSnapshot[] passFilters;
    public AudioMixerSnapshot[] defaultSnap;
    public float volume = 0;
    public Slider volumeSlider;
    void Start()
    {
        pausePrefab.SetActive(false);
        mixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);

        playerRef = (Player)FindObjectOfType<Player>();
        playerRef._actionMap.Platforming.Pause.performed += pause => PauseControl();
    }
 
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void PauseControl()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            Pause();
        }

        else
        {
            Unpause();
        }
    }
    // Update is called once per frame
    /*  
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
   */
    public bool isPaused()
    {
        return gamePaused;
    }

    public void Pause()
    {
        Debug.Log("pausing");
        Time.timeScale = 0;
        gamePaused = true;
        pausePrefab.SetActive(true);
        float[] weights = {1f};
        mixer.TransitionToSnapshots(passFilters, weights, .01f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        Debug.Log("Unpausing");
        Time.timeScale = 1;
        gamePaused = false;
        pausePrefab.SetActive(false);
        float[] weights = {1f};
        mixer.TransitionToSnapshots(defaultSnap, weights, .01f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    public void OnSliderValueChanged(float value)
    {
        volume = value;
        mixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
