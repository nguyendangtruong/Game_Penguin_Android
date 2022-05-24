using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class VolumeSoundMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetVolume(float sliderSound)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderSound) * 20);
    }

}
