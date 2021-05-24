using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{
    void Start()
    {
        AudioSource[] objs = FindObjectsOfType<AudioSource>();

        int ctr = 0;
        foreach(AudioSource a in objs)
        {
            if(a.gameObject.CompareTag("MenuMusic"))
            {
                ctr ++;
            }
        }
        if(ctr > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "IntroCutscene" && this.gameObject != null)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
