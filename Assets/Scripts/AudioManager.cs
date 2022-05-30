using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    bool _mute;
    float _masterSound;
    float _musicSound;
    float _fxSound;


    void Awake()
    {
        _masterSound = 0.5f;
        _musicSound = 1f;
        _fxSound = 1f;


        SaveSoundPrefs();
    }

    private void SaveSoundPrefs()
    {
        PlayerPrefs.SetFloat("_masterSound", _masterSound);
        PlayerPrefs.SetFloat("_musicSound", _musicSound);
        PlayerPrefs.SetFloat("_fxSound", _fxSound);
        PlayerPrefs.Save();
    }

    public void SetMute(bool value)
    {
        if (value)
        {
            _mixer.SetFloat("_masterSound", Mathf.Log10(0.001f) * 20);
        }
        if (!value)
        {
            _mixer.SetFloat("_masterSound", _masterSound);
        }
    }

    public void SetMasterVol(Slider volume)
    {
        _masterSound = volume.value;
        _mixer.SetFloat("_masterSound", Mathf.Log10(_masterSound) * 20); //Mathf.Log10(volume.value) * 20
    }

    public void SetFXVol(Slider volume)
    {
        _fxSound = volume.value;
        _mixer.SetFloat("_fxSound", Mathf.Log10(_fxSound) * 20);
    }

    public void SetMusicVol(Slider volume)
    {
        _musicSound = volume.value;
        _mixer.SetFloat("_musicSound", Mathf.Log10(_musicSound) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
