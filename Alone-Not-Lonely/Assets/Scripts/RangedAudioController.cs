using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAudioController : MonoBehaviour
{
    private AudioSource sourceToPlay;
    public Transform target;
    private bool audioPlaying;
    public float range;
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
            Debug.Log("Play Growl");
            sourceToPlay.Play();
            audioPlaying = true;
        }
        else if(audioPlaying && dist >=range){
            Debug.Log("Pause Growl");
            audioPlaying = false;
            sourceToPlay.Pause();
        }
    }
}
