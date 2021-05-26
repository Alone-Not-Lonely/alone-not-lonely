using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistence : MonoBehaviour
{
    public static ScenePersistence instance;
    private void Awake() {
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();

        if (objs.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        ScenePersistence[] objs = (ScenePersistence[])FindObjectsOfType<ScenePersistence>();

        if (objs.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Scene Loaded: " + scene.name);
        if(scene.name == "MainMenu" || scene.name == "IntroCutscene" || scene.name == "Credits" || scene.name == "Controls" || scene.name == "EndCutscene")
        {
            Destroy(this.gameObject);
        }
        SceneManager.SetActiveScene(scene);
        //Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

    private void OnDisable(){
        Debug.Log("Player disabled");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
