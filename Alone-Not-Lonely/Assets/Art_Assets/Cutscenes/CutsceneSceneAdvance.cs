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

    private void OnEnable() {
        _actionMap.Platforming.Pause.performed += EndReached;
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
        _actionMap.Platforming.Pause.performed -= EndReached;
        //Instantiate(playerCameraRig, new Vector3(10.9f, 3.33f, -3.8f), Quaternion.identity);
        Debug.Log("Skipped Cutscene");
        SceneManager.LoadScene(SceneName);
    }
    void EndReached(VideoPlayer vp)
    {
        _actionMap.Platforming.Pause.performed -= EndReached;
        //Instantiate(playerCameraRig, new Vector3(10.9f, 3.33f, -3.8f), Quaternion.identity);
        Debug.Log("Skipped Cutscene");
        SceneManager.LoadScene(SceneName);
    }
    private void OnDisable() {
        _actionMap.Platforming.Pause.performed -= endCutscene => EndReached(video);
    }
}
