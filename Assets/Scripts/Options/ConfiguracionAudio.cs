using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfiguracionAudio : MonoBehaviour
{
    public AudioMixer masterMixer;
    float fxValue, musicaValue, masterValue;
    public Slider fxSlider, musicaSlider, masterSlider; 

    private void Start()
    {
        fxValue = PlayerPrefs.GetFloat("FxValue", 1);
        musicaValue = PlayerPrefs.GetFloat("MusicaValue", 1);
        masterValue = PlayerPrefs.GetFloat("MasterValue", 1);
        SetVolumenValue();
    }

    void SetVolumenValue()
    {
        masterMixer.SetFloat("FXVolumen", Mathf.Log10(fxValue) * 20);
        masterMixer.SetFloat("MusicaVolumen", Mathf.Log10(musicaValue) * 20);
        masterMixer.SetFloat("MasterVolumen", Mathf.Log10(masterValue) * 20);

        fxSlider.value = fxValue;
        musicaSlider.value = musicaValue;
        masterSlider.value = masterValue;
    }

    public void SetFXVolume(float value)
    {
        PlayerPrefs.SetFloat("FxValue", value);
        masterMixer.SetFloat("FXVolumen", Mathf.Log10(value) * 20);
    }
    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicaValue", value);
        masterMixer.SetFloat("MusicaVolumen", Mathf.Log10(value) * 20);
    }
    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("MasterValue", value);
        masterMixer.SetFloat("MasterVolumen", Mathf.Log10(value) * 20);
    }
}
