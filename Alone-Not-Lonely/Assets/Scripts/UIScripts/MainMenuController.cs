using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string SceneName;
    public GameObject MenuMusic;
    AudioSource sfx;
    public AudioClip[] clips = new AudioClip[2];

    private void Awake() {
        AudioSource[] objs = FindObjectsOfType<AudioSource>();
        sfx = GetComponent<AudioSource>();
        if(objs.Length <= 2)
        {
            //first time in the menu
            sfx.PlayOneShot(clips[0]);
        }
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] objs = FindObjectsOfType<AudioSource>();
        foreach(AudioSource a in objs)
        {
            if(a.gameObject.CompareTag("MenuMusic"))
            {
                MenuMusic = a.gameObject;
            }
        }
        Debug.Log(MenuMusic);
        //MenuMusic = FindObjectOfType<AudioSource>().gameObject;
    }

    IEnumerator PlaySound(string name)
    {
        sfx.PlayOneShot(clips[1]);
        yield return new WaitWhile (()=> sfx.isPlaying);
        SceneManager.LoadScene(name);
    }

    public void LoadGame(string scenenamepass)
    {   if(scenenamepass == "IntroCutscene")
        {
            Destroy(MenuMusic);
        }
        //StartCoroutine(PlaySound(scenenamepass));
        SceneManager.LoadScene(scenenamepass);
    }

    public void LoadGame()
    {
        if(SceneName == "IntroCutscene")
        {
            DestroyImmediate(MenuMusic);
        }
        StartCoroutine(PlaySound(SceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
