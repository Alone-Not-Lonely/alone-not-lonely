using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HamperPersistence : MonoBehaviour
{
    public Vector3 smashPosition = new Vector3(7.63999987f,0.280000001f,-21.4599991f);
    public Vector3 bedPosition = new Vector3(-8.36999989f,0.280000001f,-57.8699989f);
    void Awake()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(ProgressionTracker.instance.alreadyVisited.Contains("Bedroom1"))
        {
            this.transform.position = bedPosition;
        }
        else
        {
            this.transform.position = smashPosition;
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
