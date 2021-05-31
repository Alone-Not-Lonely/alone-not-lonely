using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionTracker : MonoBehaviour
{
    public List<string> alreadyVisited = new List<string>();
    public static ProgressionTracker instance;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void MarkSceneCompleted(string name)
    {
        alreadyVisited.Add(name);
    }
}
