using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneSceneAdvance : MonoBehaviour
{
    private VideoPlayer video;
    public string SceneName;
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneName);
    }
}
