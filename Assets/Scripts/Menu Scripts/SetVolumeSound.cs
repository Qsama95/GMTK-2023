using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class SetVolumeSound : MonoBehaviour
{
    public AudioMixer soundMixer;

    public void SetLevel(float sliderValue)
    {
        soundMixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
    }
}
