using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneSceneAdvance : MonoBehaviour
{
    private VideoPlayer video;
    public string SceneName;
    public DefaultControls _actionMap;
    void Awake() {
        _actionMap = new DefaultControls();
        _actionMap.Enable();
    }
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += EndReached;
        _actionMap.Platforming.Pause.performed += endCutscene => EndReached(video);
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneName);
    }
}
