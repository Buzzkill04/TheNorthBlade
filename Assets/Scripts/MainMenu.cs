using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;
    //The error screen game object and the text on it
    public GameObject errorScreen;
    public Text errorScreenText;
    public AudioMixer musicAudioMixer;
    public AudioMixer sfxAudioMixer;
    public static bool isMultiplayer = false;

    // Start is called before the first frame update
    private void Start()
    {
        //Set the values to the music and SFX to the saved player value
        musicAudioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        sfxAudioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("SFXV"));
    }

    //Called when continue game is called
    public void ContinueGame()
    {
        try
        {
            //Load the player data
            PlayerData savedPlayerData = SaveSystem.LoadPlayer();
            //Move to the saved scene
            StartCoroutine(LoadNextScene(savedPlayerData.sceneBuildIndex));
        }
        catch (Exception)
        {
            //If there was no save file found enable the error screen
            errorScreen.SetActive(true);
            errorScreenText.text = "No Save File Found!";
        }
        

    }
    //Called when new game is pressed
    public void NewGame()
    {
        SaveSystem.DeleteSaveFile();
        //Reset the character type and prefab name
        CharacterCreator.characterType = null;
        CharacterCreator.characterPrefabName = null;
        //Load the next scene, on a new game the scene should be the character creator,
        //The character creator has a build index of 1
        StartCoroutine(LoadNextScene(1));
    }
    //Called when multiplayer is pressed
    public void Multiplayer()
    {
        //set the multiplayer variable to true, so that other scripts know multiplayer was selected
        isMultiplayer = true;
        //Reset the character creator statics
        CharacterCreator.characterPrefabName = null;
        CharacterCreator.characterType = null;
        CharacterCreator.MultiplayerCharacterPrefabName = null;
        CharacterCreator.MultiplayerCharacterType = null;
        //The character creator has a build index of 1
        StartCoroutine(LoadNextScene(1));
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    //This coroutine handles UI fading and level changing a coroutine is used so waitforseconds can be used
    IEnumerator LoadNextScene(int sceneIndex)
    {
        //Fade the screen to black,
        animator.SetTrigger("Start");
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        //Load the character creator
        SceneManager.LoadScene(sceneIndex);
    }
}
