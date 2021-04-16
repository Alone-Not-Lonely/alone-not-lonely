using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuControllerCutscene : MonoBehaviour
{
    private bool gamePaused;

    public GameObject pausePrefab;

    private List<GameObject> pauseComponents = new List<GameObject>();

    private CutsceneSceneAdvance playerRef;
    public AudioMixer mixer;
    public AudioMixerSnapshot[] passFilters;
    public AudioMixerSnapshot[] defaultSnap;
    public float volume = 0;
    public Slider volumeSlider;
    public bool startActive = false;
    void Start()
    {
        //pausePrefab.SetActive(false);
        pausePrefab = this.gameObject;
        mixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);

        playerRef = (CutsceneSceneAdvance)FindObjectOfType<CutsceneSceneAdvance>();
        playerRef._actionMap.Platforming.Pause.performed += pause => PauseControl();
        foreach (Transform child in pausePrefab.transform) {
            pauseComponents.Add(child.gameObject);
            child.gameObject.SetActive(startActive);
        }
    }
 
    void Awake()
    {
        if(startActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
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

    public bool isPaused()
    {
        return gamePaused;
    }

    public void Pause()
    {
        //Debug.Log("pausing");
        Time.timeScale = 0;
        gamePaused = true;
        //pausePrefab.SetActive(true);
        foreach(GameObject child in pauseComponents)
        {
            child.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        playerRef._actionMap.Platforming.Pause.performed -= pause => PauseControl();
        //Debug.Log("Unpausing");
        Time.timeScale = 1;
        gamePaused = false;
        //pausePrefab.SetActive(false);
        foreach(GameObject child in pauseComponents)
        {
            child.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerRef.PlayVideo();
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
