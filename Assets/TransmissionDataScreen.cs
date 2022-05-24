using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionDataScreen : MonoBehaviour
{
    public float volumeSound;
    public Slider sliderVolSound;

    public void SaveVolumeSound()
    {
        //Lấy setting từ slider âm thanh trong Option
        volumeSound = sliderVolSound.value;
        //Truyền dữ liệu qua Screen game
        PlayerPrefs.SetFloat("setVolumeSound", volumeSound);
        Debug.Log(volumeSound);

    }
    private void OnDestroy()
    {
        SaveVolumeSound();
    }
}
