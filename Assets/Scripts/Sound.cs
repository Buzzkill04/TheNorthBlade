using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//This class is similar to the player data class, in which is serves as a template for a sound file
//System.serioalizable allows the non unity class to show in the editor
[System.Serializable]
public class Sound
{
    //The sound clip
    public AudioClip audioClip;
    public AudioMixerGroup audioMixerGroup;
    //The name of the clip
    public string clipName;
    //The type of the clip whether it is SFX or Music
    public string clipType;
    //The audio source playing the clip
    [HideInInspector]
    public AudioSource clipSource;
    
}
