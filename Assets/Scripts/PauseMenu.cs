using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;

    //Whether the game is paused
    public static bool paused = false;

    private void Update()
    {
        if (paused)
        {
            PauseGame();
            
        }
        else
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        //Play the game by resuming the time scale
        Time.timeScale = 1f;
        //set paused to false
        paused = false;
    }

    void PauseGame()
    {
        //Pause the game by setting the time scale to 0
        Time.timeScale = 0f;
        //set paused to true
        paused = true;
    }
    public void ResumePressed()
    {
        //resume the game
        ResumeGame();
        //Set the pause menu to false (close the menu)
        gameObject.SetActive(false);

    }
    public void ExitButton()
    {
        //resume the game
        ResumeGame();
        //Quit
        Application.Quit();
    }

    //Called when main menu is pressed
    public void MainMenu()
    {
        //resume the game
        ResumeGame();
        //Load the next scene, on a new game the scene should be the character creator,
        //The main menu has a build index of 0
        StartCoroutine(LoadNextScene(0));
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
