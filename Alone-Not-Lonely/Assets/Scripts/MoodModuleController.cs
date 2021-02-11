using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MoodModuleController : MonoBehaviour
{
    private AudioSource[] music;
    private int currentSource = 0;

    private float maxVolume;
    private bool fadingVolume;
    public float fadeSpeed = .01f;

    private AudioMixer mixer;
    void Start()
    {
        music = GetComponents<AudioSource>();
        mixer = ((PauseMenuController)FindObjectOfType<PauseMenuController>()).mixer;
        maxVolume = 1;
        music[0].volume = maxVolume;
        fadingVolume = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(music[currentSource].volume >= maxVolume && music[(currentSource + 1) % 2].volume <= 0f)
        {
            StopCoroutine(SwapVolumes());
            fadingVolume = false;
            //music[(currentSource + 1) % 2].Stop();
        }
        if(!fadingVolume  && music[currentSource].volume != maxVolume)
        {
            music[currentSource].volume = maxVolume;
        }
    }

    public void ChangeSong(AudioClip newClip)
    {
        var currTime = music[currentSource].time;
        music[(currentSource + 1) % 2].clip = newClip;
        music[(currentSource + 1) % 2].Play();
        music[(currentSource + 1) % 2].time = currTime;
        currentSource = (currentSource + 1) % 2;
        StartCoroutine(SwapVolumes());
    }
    
    IEnumerator SwapVolumes()
    {
        while(music[currentSource].volume < maxVolume && music[(currentSource + 1) % 2].volume > 0f)
        {
            fadingVolume = true;
            music[currentSource].volume += fadeSpeed;
            music[(currentSource + 1) % 2].volume -= fadeSpeed;
            yield return null;
        }
    }

}
