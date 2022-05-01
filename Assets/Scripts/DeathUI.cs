using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;

    public void ExitButton()
    {
        Application.Quit();
    }

    //Called when main menu is pressed
    public void MainMenu()
    {
        //Load the next scene, on a new game the scene should be the character creator,
        //The main menu has a build index of 0
        StartCoroutine(LoadNextScene(0));
    }

    //Called when load last save is pressed
    public void LoadLastSave()
    {
        //Get the players saved data
        PlayerData savedPlayerData = SaveSystem.LoadPlayer();
        //Travel to the level they were on when they last saved
        StartCoroutine(LoadNextScene(savedPlayerData.sceneBuildIndex));
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
