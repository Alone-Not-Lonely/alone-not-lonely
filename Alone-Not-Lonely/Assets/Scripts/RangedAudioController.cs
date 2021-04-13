using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAudioController : MonoBehaviour
{
    private AudioSource sourceToPlay;
    public Transform target;
    private bool audioPlaying;
    public float range;

    private bool fadingIn = false;
    private bool fadingOut = false;
    public float fadeSpeed = .01f;
    void Start()
    {
        sourceToPlay = GetComponent<AudioSource>();
        target = ((Player)FindObjectOfType(typeof(Player))).gameObject.transform;
        sourceToPlay.loop = true;
        audioPlaying = false;
    }
    void FixedUpdate()
    {
        float dist = Vector3.Distance(this.transform.position, target.position);
        if(!audioPlaying && dist < range){
            sourceToPlay.volume = 0;
            sourceToPlay.Play();
            StartCoroutine(FadeIn());
            audioPlaying = true;
        }
        else if(audioPlaying && dist >=range){
            StartCoroutine(FadeOut());
        }
        if(audioPlaying && fadingOut && sourceToPlay.volume <=0)
        {
            StopCoroutine(FadeOut());
            fadingOut = false;
            audioPlaying = false;
            sourceToPlay.Pause();
        }
        if(audioPlaying && fadingIn && sourceToPlay.volume >= 1)
        {
            StopCoroutine(FadeIn());
            fadingIn = false;
        }
    }

    IEnumerator FadeIn()
    {
        while(sourceToPlay.volume < 1)
        {
            fadingIn = true;
            sourceToPlay.volume += fadeSpeed;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while(sourceToPlay.volume > 0)
        {
            fadingOut = true;
            sourceToPlay.volume -= fadeSpeed;
            yield return null;
        }
    }
}
