using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistence : MonoBehaviour
{
    private void Awake() {
        ScenePersistence[] objs = (ScenePersistence[])Resources.FindObjectsOfTypeAll(typeof(ScenePersistence));
        Debug.Log(this.name);

        if (objs.Length > 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.gameObject);//Keep persistent
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu" || scene.name == "IntroCutscene" || scene.name == "Credits" || scene.name == "Controls")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
