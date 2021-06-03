using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixtapeController : MonoBehaviour
{
    AudioClip saveClip;
    public AudioClip oneShot;
    AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = FindObjectOfType<AudioSource>();
    }

    public void SpitFire()
    {
        Debug.Log("Returning to normal in " + oneShot.length);
        saveClip = musicSource.clip;
        musicSource.clip = oneShot;
        musicSource.Play();
        Invoke("SwitchBack", oneShot.length);
        GetComponent<Button>().enabled = false;
    }

    void SwitchBack()
    {
        musicSource.clip = saveClip;
        musicSource.Play();
    }
}
