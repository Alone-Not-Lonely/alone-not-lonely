using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuVolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;
    public float volume = 0;
    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);
    }

    public void OnSliderValueChanged(float value)
    {
        volume = value;
        mixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
