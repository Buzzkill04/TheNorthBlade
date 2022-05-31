using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    //The animator for the screen fading
    public Animator animator;
    //The players save file
    public PlayerData playerSaveFile;
    //The text fields for the stats menu
    public Text maxHealthText;
    public Text strengthText;
    public Text levelText;
    public Text enemiesKilledText;
    //Players level
    private int playerLevel;
    //The max a players health can be
    private float maxPlayerHealth;
    //Players Strength
    private float playerStrength;
    //Amount of enemies the player has killed
    private int enemiesKilled;


    // Start is called before the first frame update
    void Start()
    {
        //Get the players save file
        playerSaveFile = SaveSystem.LoadPlayer();
        //Get the level, strength and amount of enemies killed
        playerLevel = playerSaveFile.playerLevel;
        playerStrength = playerSaveFile.playerStrength;
        enemiesKilled = playerSaveFile.enemyKillCount;
        //Calculate the max health of the player
        maxPlayerHealth = 100 * playerLevel;
        //Set the text inside the text box to the appropriate values
        maxHealthText.text = maxPlayerHealth.ToString();
        strengthText.text = playerStrength.ToString();
        levelText.text = playerLevel.ToString();
        enemiesKilledText.text = enemiesKilled.ToString();
        SaveSystem.DeleteSaveFile();
    }

    public void MainMenu()
    {
        //Load the next scene, on a new game the scene should be the character creator,
        //The main menu has a build index of 0
        StartCoroutine(LoadNextScene(0));
    }

    public void NewGame()
    {
        //Reset the character type and prefab name
        CharacterCreator.characterType = null;
        CharacterCreator.characterPrefabName = null;
        //Load the next scene, on a new game the scene should be the character creator,
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
