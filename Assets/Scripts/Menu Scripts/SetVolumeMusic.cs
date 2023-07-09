using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class SetVolumeMusic : MonoBehaviour
{
    public AudioMixer musicMixer;

    public void SetLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

}
