using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour
{
    //The deathUI to display when a player dies
    public GameObject DeathUI;
    //The player life scripts of both players
    private PlayerLife player1Life;
    private PlayerLife player2Life;
    //The sliders and current health of both players
    public Slider player1HealthSlider;
    public Text player1CurrHealth;
    public Slider player2HealthSlider;
    public Text player2CurrHealth;
    //The text on the death UI about the winning players
    public Text deathUIWinnerText;
    //The UI animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Get the player life scripts
        player1Life = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        player2Life = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the player health variables
        float player1Health = player1Life.playerHealth;
        float player2Health = player2Life.playerHealth;
        //set the values of the sliders and the text of the current health text boxes
        player1HealthSlider.value = player1Health;
        player1CurrHealth.text = player1Health.ToString();
        player2HealthSlider.value = player2Health;
        player2CurrHealth.text = player2Health.ToString();
        //If either players health is less than 0
        if (player1Health <= 0 || player2Health <= 0 )
        {
            //Open the death UI
            DeathUI.SetActive(true);
            //Set the text to either player 1 wins or player 2 wins. ? is a shorthand if statement
            //If player 2 health is less than 0, player 1 wins is displayed, else player 2 wins is displayed
            deathUIWinnerText.text = player2Health <= 0 ? "Player 1 Wins" : "Player 2 Wins";
        }
    }

    public void PlayAgain()
    {
        //Start the duel
        StartCoroutine(LoadNextScene(9));
    }

    public void GoToMainMenu()
    {
        //Reset the values to null so that it doesnt mess up with loading the players character
        //If the player chooses to play singleplayer
        CharacterCreator.characterPrefabName = null;
        CharacterCreator.characterType = null;
        CharacterCreator.MultiplayerCharacterPrefabName = null;
        CharacterCreator.MultiplayerCharacterType = null;
        //Load the next scene, on a new game the scene should be the character creator,
        //The main menu has a build index of 0
        StartCoroutine(LoadNextScene(0));
        MainMenu.isMultiplayer = false;
    }
    //Called when exit is pressed
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator LoadNextScene(int sceneIndex)
    {
        //Fade the screen to black,
        animator.SetTrigger("Start");
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        //Load the starting screen
        SceneManager.LoadScene(sceneIndex);
    }
}
