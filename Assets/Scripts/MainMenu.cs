using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;
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
            Debug.Log("No save file found!");
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
