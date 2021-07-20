using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Toggle _checkBoxFullScreen;
    private bool isFullScreen;

    [SerializeField] private Slider sliderAudioGeneral;
    [SerializeField] private Slider sliderAudioMusic;
    [SerializeField] private Slider sliderAudioSound;
    [SerializeField] private Slider sliderAudioAmbiance;
    [SerializeField] AudioMixer _audioMixer;

    void Start()
    {
        isFullScreen = Screen.fullScreen;

        _checkBoxFullScreen.isOn = isFullScreen;

        float _general;
        _audioMixer.GetFloat("Master", out _general);
        sliderAudioGeneral.value = _general;

        float _music;
        _audioMixer.GetFloat("Music", out _music);
        sliderAudioMusic.value = _music;


        float _sound;
        _audioMixer.GetFloat("Sound", out _sound);
        sliderAudioSound.value = _sound;

        float _ambiance;
        _audioMixer.GetFloat("Ambiance", out _ambiance);
        sliderAudioAmbiance.value = _ambiance;
    }


    public void FullScreenToggle()
    {
        if (Screen.fullScreen != _checkBoxFullScreen.isOn)
        {
            isFullScreen = !isFullScreen;
            Screen.fullScreen = isFullScreen;
        }
    }

    public void AudioVolumeGeneral(float sliderValue)
    {
        _audioMixer.SetFloat("Master", sliderValue);
    }
    public void AudioVolumeMusic(float sliderValue)
    {
        _audioMixer.SetFloat("Music", sliderValue);
    }
    public void AudioVolumeSound(float sliderValue)
    {
        _audioMixer.SetFloat("Sound", sliderValue);
    }
    public void AudioVolumeAmbiance(float sliderValue)
    {
        _audioMixer.SetFloat("Ambiance", sliderValue);
    }
}
