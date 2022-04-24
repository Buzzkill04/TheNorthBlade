using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;

    public void PlayGame()
    {
        //Start the coroutine to start a new game
        StartCoroutine(LoadStart());
    }
    IEnumerator LoadStart()
    {
        //Fade the screen to black,
        animator.SetTrigger("Start");
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        //Load the starting screen, will be replaced with the character creator when ready
        SceneManager.LoadScene("DarkForest");
    }
}
