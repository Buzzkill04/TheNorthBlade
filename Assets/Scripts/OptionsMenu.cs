    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //Get the sliders and audio mixer so the values can be set
    public Slider sfxSlider;
    public Slider musicSlider;
    public AudioMixer musicAudioMixer;
    public AudioMixer sfxAudioMixer;

    //Called when the script is being loaded
    private void Awake()
    {
        //When the script is being loaded, set the value of both sliders to their stored values
        sfxSlider.value = PlayerPrefs.GetFloat("SFXV");
        musicSlider.value = PlayerPrefs.GetFloat("Volume");
    }
    //Called when the value of the music slider is changed
    public void MusicSlider(float value)
    {
        //Set the audio mixers volume to the value, value of slider is between -80 and 0,
        //These are the min/max values of the audio mixer
        musicAudioMixer.SetFloat("Volume", value);
        //Set the player prefs Volume value to the sliders value
        PlayerPrefs.SetFloat("Volume", value);
        
    }
    //Called when the value of the SFX slider is changed
    public void SFXSlider(float value)
    {
        //Set the audio mixers volume to the value, value of slider is between -80 and 0,
        //These are the min/max values of the audio mixer
        sfxAudioMixer.SetFloat("Volume", value);
        //Set the player prefs Volume value to the sliders value
        PlayerPrefs.SetFloat("SFXV", value);
    }
    //Called when the option menu is closed
    public void OptionsSave()
    {
        //Write the player prefs to disk
        PlayerPrefs.Save();
    }
}
