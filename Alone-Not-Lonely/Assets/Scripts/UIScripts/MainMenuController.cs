using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string SceneName;
    public GameObject MenuMusic;
    // Start is called before the first frame update
    void Start()
    {
        MenuMusic = FindObjectOfType<AudioSource>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame(string scenenamepass)
    {   if(scenenamepass == "Attic2")
        {
            Destroy(MenuMusic);
        }
        SceneManager.LoadScene(scenenamepass);
    }

    public void LoadGame()
    {
        if(SceneName == "Attic2")
        {
            Destroy(MenuMusic);
        }
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
