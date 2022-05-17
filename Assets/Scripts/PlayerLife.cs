using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script holds all the infomation about the players life
public class PlayerLife : MonoBehaviour
{
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerMovement playerMovementScript;
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerCombat playerCombatScript;
    //Reference to the inventory manager connected to the game object
    private InventoryManager inventoryManagerScript;
    //The level loader game object
    private GameObject levelUI;
    //Animator component of the sprite
    private Animator animator;
    //The animator of the effects game object
    public Animator effectsAnimator;
    //Players XP
    public float playerXP = 0f;
    //Players level
    public int playerLevel = 1;
    //Players health
    public float playerHealth;
    //The max a players health can be
    public float maxPlayerHealth;
    //Players Strength
    public float playerStrength = 1f;
    //Player ability status
    public int abilityStatus = 0;
    //Amount of enemies the player has killed
    public int enemiesKilled = 0;
    //The character type, will be chosen in the character creator
    public string characterType;
    //The prefab of the player will also be chosen in the character creator
    public string characterPrefabName;
    //The amount of pinapples the player has 
    public int numPinapple = 0;
    //The amount of peaches the player has
    public int numPeach = 0;
    //The amount of Strawberrys the player has 
    public int numStrawberry = 0;
    //The scene the player is playing
    public int sceneBuildIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Get the scene build index
        sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        //Set the character type and the prefab name
        characterType = CharacterCreator.characterType;
        characterPrefabName = CharacterCreator.characterPrefabName;
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
        //Get the playerCombat script that is connected to the game object the script is attached to.
        playerCombatScript = GetComponent<PlayerCombat>();
        //Get the playerCombat script that is connected to the game object the script is attached to.
        inventoryManagerScript = GetComponent<InventoryManager>();
        //Get the level loader game object
        levelUI = GameObject.Find("LevelUI");
        //Set the animator variable to the playerMovement scripts animator.
        animator = playerMovementScript.animator;
        try
        {
            LoadProgress();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            playerHealth = 100f;
        }
        maxPlayerHealth = 100 * playerLevel;
        playerCombatScript.playerAttackDamage = 30 * playerStrength;
        playerCombatScript.characterAbilityStatus = abilityStatus;
    }

    // Update is called with the physics system 50 times per second
    void FixedUpdate()
    {
        //Get the amount of food the player has picked up
        numPinapple = inventoryManagerScript.numPinapple;
        numPeach = inventoryManagerScript.numPeach;
        numStrawberry = inventoryManagerScript.numStrawberry;
        //Get the character ability status
        abilityStatus = playerCombatScript.characterAbilityStatus;
        //Set the animator Health parameter to the players current health every fram
        animator.SetFloat("Health", playerHealth);
        //Check if the player health drops below 0
        if (playerHealth <= 0)
        {
            //Start the death animation
            animator.SetTrigger("Death");
            //If the players chosen type is swordsman and their ability is at 7 - playerlevel, so as the player
            //levels up, the can use their ability more often
            if (characterType == "swordsman" && playerCombatScript.characterAbilityStatus == (7 - playerLevel))
            {
                //revive them
                playerCombatScript.SMAbility();
            }
            else
            {
                //Otherwise, kill them
                PlayerDeath();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
        //If the player xp equals 10 level them up
        if (playerXP == 5)
        {
            //level the player   
            playerLevel++;
            //update the max health
            maxPlayerHealth = 100 * playerLevel;
            //Reset the player XP
            playerXP = 0;
            //Play the level up animation
            effectsAnimator.SetTrigger("LevelUp");
            //Play the level up sound
            FindObjectOfType<AudioManager>().playSound("levelUp");
        }
    }

    //Called when player takes damage
    public void PlayerTakeDamage(float damage)
    {
        //set hurt animator paramater take away HP
        animator.SetTrigger("Hurt");
        playerHealth -= damage;
        //Play the hit sound
        FindObjectOfType<AudioManager>().playSound("hit");
    }

    //Called when player heals
    public void PlayerHeal(float healAmount)
    {
        playerHealth += healAmount;
        //Play the heal sound
        FindObjectOfType<AudioManager>().playSound("heal");
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
    }

    //Called when a player dies
    public void PlayerDeath()
    {
        //If multiplayer is false, this is done becuase the way the death menu is opened is different 
        //in a multiplayer battle
        if (!MainMenu.isMultiplayer)
        {
            //Get the levelUI game object, then get the deathUI (index 3), this will get the transform
            //So we need to get the original game object and set it to being active
            levelUI.transform.GetChild(3).gameObject.SetActive(true);
        }
        //Destroy the playerMovementScript so that the player is unable to move.
        Destroy(playerMovementScript);
    }
    //Save the progress of the player
    public void SaveProgress()
    {
        //Call the save player data method, 'this' refers to this script
        SaveSystem.SavePlayerData(this);
    }
    //Load the progress of the player
    public void LoadProgress()
    {
        //Call the load player method and store the return value
        PlayerData savedPlayerData = SaveSystem.LoadPlayer();
        characterType = savedPlayerData.characterType;
        characterPrefabName = savedPlayerData.characterPrefabName;
        playerLevel = savedPlayerData.playerLevel;
        playerXP = savedPlayerData.playerXP;
        playerHealth = savedPlayerData.playerHealth;
        playerStrength = savedPlayerData.playerStrength;
        sceneBuildIndex = savedPlayerData.sceneBuildIndex;
        //Make the ability status from the save the combat scripts ability status
        abilityStatus = savedPlayerData.characterAbilityStatus;
        enemiesKilled = savedPlayerData.enemyKillCount;
        //Get the amount of collected items and set the values in the inventory manager script.
        inventoryManagerScript.numPinapple = savedPlayerData.inventoryItemAmounts[0];
        inventoryManagerScript.numPeach = savedPlayerData.inventoryItemAmounts[1];
        inventoryManagerScript.numStrawberry = savedPlayerData.inventoryItemAmounts[2];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collision has the tag End
        if (collision.CompareTag("End"))
        {
            //Calculate the next scene's build index
            Scene currentScene = SceneManager.GetActiveScene();
            int nextSceneIndex = currentScene.buildIndex + 1;
            //Start the coroutine to load the next level
            StartCoroutine(LoadNextLevel(nextSceneIndex));
        }
        //If the collision has the tag spikes 
        if (collision.CompareTag("WaterDeath"))
        {
            //Kill the player by taking the rest of the players health away
            PlayerTakeDamage(playerHealth);
        }
        if (collision.CompareTag("StoryItem"))
        {
            //Load the final menu, passing in the game object of the collision
            StartCoroutine(LoadFinalMenu(collision.gameObject));
        }
    }
    //This coroutine handles UI fading and level changing a coroutine is used so waitforseconds can be used
    IEnumerator LoadNextLevel(int levelIndex)
    {
        //Get the animator of the level loader
        Animator UIanimator = levelUI.GetComponentInChildren<Animator>();
        //Set the current scene build index into the variable that is saved in the save file
        sceneBuildIndex = levelIndex;
        //Save the players progress
        SaveProgress();
        //Start the fade to black animation
        UIanimator.SetTrigger("Start");
        //Wait for the time needed to complete the animation
        yield return new WaitForSeconds(1);
        //Load the new scene
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator LoadFinalMenu(GameObject TheNorthBlade)
    {
        TheNorthBlade northBladeScript = TheNorthBlade.GetComponent<TheNorthBlade>();
        northBladeScript.collected = true;
        //Get the animator of the level loader
        Animator UIanimator = levelUI.GetComponentInChildren<Animator>();
        //Start the fade to white animation
        UIanimator.SetTrigger("EndGame");
        //Wait for the time needed to complete the animation
        yield return new WaitForSeconds(4);
        //Load the end menu
        SceneManager.LoadScene("EndMenu");
    }


}
