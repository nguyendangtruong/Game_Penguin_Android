using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSoundInGame : MonoBehaviour
{
    public AudioSource audioSoundBG;
    private float volume;

    void Start()
    {
        //Lấy giá trị setting âm thanh từ Menu truyền sang
        volume = PlayerPrefs.GetFloat("setVolumeSound");
        //Điều chỉnh âm thanhphù hợp với Setting
        SetVolSoundInGame();

    }

    void SetVolSoundInGame()
    {
        audioSoundBG.volume = audioSoundBG.volume * (volume / 1);
    }

    
}
