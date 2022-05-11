using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    //The array of sounds that can be played
    public Sound[] sounds;
    //The current instance of the audio manager
    public static AudioManager currentInstance;

    //Called when the script is being loaded
    private void Awake()
    {
        //If the current instance is null
        if (currentInstance == null)
        {
            //Set the current instance to the instance of this script
            currentInstance = this;
        }
        else //else
        {
            //Destroy the game object, this is to avoid multiple AudioManagers from being created
            Destroy(gameObject);
        }

        //Dont destroy the audio manager when switching between scenes
        DontDestroyOnLoad(gameObject);

        //Foreach sound in the sound array
        foreach (Sound s in sounds)
        {
            //add a AudioSource to the game object script is attached to
            s.clipSource = gameObject.AddComponent<AudioSource>();
            //Set the clip of the AudioSource to the audio clip of the current sound in the iteration
            s.clipSource.clip = s.audioClip;
            //set the audio mixer of the AudioSource to the designated mixer group of the current sound
            s.clipSource.outputAudioMixerGroup = s.audioMixerGroup;
            //If the clip is music
            if (s.clipType == "Music")  
            {
                //Loop the audio source
                s.clipSource.loop = true;
            }           
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Play the music audio clip
        playSound("music");
    }

    public void playSound(string clipName)
    {
        try
        {
            //try find the sound to play in the array with a lambda function
            Sound soundToPlay = Array.Find(sounds, sound => sound.clipName == clipName);
            //Play the sound
            soundToPlay.clipSource.Play();
        }
        catch (Exception e)
        {
            //If there was no sound by the passed in clip name, throw an error to the console
            Debug.Log($"No sound file found {e}");            
        }
    }

    
}
