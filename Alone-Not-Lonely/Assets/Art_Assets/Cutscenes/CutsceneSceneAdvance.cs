using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CutsceneSceneAdvance : MonoBehaviour
{
    private VideoPlayer video;
    public string SceneName;
    public DefaultControls _actionMap;
    public GameObject UI;

    private void OnEnable() {
        _actionMap.Platforming.Jump.performed += EndReached;
    }
    private void Awake() {
        _actionMap = new DefaultControls();
        _actionMap.Enable();
    }
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        video.Play();
    }

    void EndReached(InputAction.CallbackContext context)
    {
        _actionMap.Platforming.Jump.performed -= EndReached;
        //Instantiate(playerCameraRig, new Vector3(10.9f, 3.33f, -3.8f), Quaternion.identity);
        Debug.Log("Skipped Cutscene");
        video.gameObject.SetActive(false);
        UI.SetActive(false);
        SceneManager.LoadScene(SceneName);
    }
    void EndReached(VideoPlayer vp)
    {
        _actionMap.Platforming.Jump.performed -= EndReached;
        //Instantiate(playerCameraRig, new Vector3(10.9f, 3.33f, -3.8f), Quaternion.identity);
        Debug.Log("Skipped Cutscene");
        video.gameObject.SetActive(false);
        if(UI)
            UI.SetActive(false);
        SceneManager.LoadScene(SceneName);
    }
    private void OnDisable() {
        _actionMap.Platforming.Jump.performed -= endCutscene => EndReached(video);
    }
}
