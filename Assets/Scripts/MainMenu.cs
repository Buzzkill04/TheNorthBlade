using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;
    //The error screen game object and the text on it
    public GameObject errorScreen;
    public Text errorScreenText;

    //Called when continue game is called
    public void ContinueGame()
    {
        try
        {
            PlayerData savedPlayerData = SaveSystem.LoadPlayer();
            StartCoroutine(LoadNextScene(savedPlayerData.sceneBuildIndex));
        }
        catch (Exception)
        {
            errorScreen.SetActive(true);
            errorScreenText.text = "No Save File Found!";
        }
        

    }
    //Called when new game is pressed
    public void NewGame()
    {
        SaveSystem.DeleteSaveFile();
        //Load the next scene, on a new game the scene should be the character creator,
        //The character creator has a build index of 1
        StartCoroutine(LoadNextScene(1));
    }
    //Called when multiplayer is pressed
    public void Multiplayer()
    {
        //TODO
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
