using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    private bool gamePaused;

    public GameObject pausePrefab;

    private List<GameObject> pauseComponents;

    private Player playerRef;
    public AudioMixer mixer;
    public AudioMixerSnapshot[] passFilters;
    public AudioMixerSnapshot[] defaultSnap;
    public float volume = 0;
    public Slider volumeSlider;
    public bool startActive = false;
    void Start()
    {
        //pausePrefab.SetActive(false);
        pauseComponents = new List<GameObject>();
        pausePrefab = this.gameObject;
        //mixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);
        float volumeOut = .5f;
        mixer.GetFloat("Volume", out volumeOut);
        volumeSlider.value = Mathf.Pow(10, volumeOut/20);

        playerRef = (Player)FindObjectOfType<Player>();
        playerRef._actionMap.Platforming.Pause.performed += pause => PauseControl();
        foreach (Transform child in pausePrefab.transform) {
            pauseComponents.Add(child.gameObject);
            child.gameObject.SetActive(startActive);
        }
        GetComponent<AudioSource>().enabled = false;
        
    }

    private void OnDestroy()
    {
        playerRef._actionMap.Platforming.Pause.performed -= pause => PauseControl();
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
        float volumeOut = .5f;
        mixer.GetFloat("Volume", out volumeOut);
        volumeSlider.value = Mathf.Pow(10, volumeOut/20);
        Debug.Log(Mathf.Pow(10, volumeOut/20));
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
        float[] weights = {1f};
        mixer.TransitionToSnapshots(passFilters, weights, .01f);
        GetComponent<AudioSource>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        //Debug.Log("Unpausing");
        Time.timeScale = 1;
        gamePaused = false;
        //pausePrefab.SetActive(false);
        foreach(GameObject child in pauseComponents)
        {
            child.SetActive(false);
        }
        float[] weights = {1f};
        mixer.TransitionToSnapshots(defaultSnap, weights, .01f);
        GetComponent<AudioSource>().enabled = false;

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
        Debug.Log("SliderValueChanged");
        volume = value;
        mixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        Debug.Log(volumeSlider.value);
    }
}
