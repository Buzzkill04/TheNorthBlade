using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script holds all the infomation about the players life
public class PlayerLife : MonoBehaviour
{
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerMovement playerMovementScript;
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerCombat playerCombatScript;
    //Reference to the inventory manager connected to the game object
    private InventoryManager inventoryManagerScript;
    //Animator component of the sprite
    private Animator animator;
    //Players XP
    public float playerXP = 0f;
    //Players level
    public int playerLevel = 1;
    //Players health
    public float playerHealth = 100f;
    //Players Strength
    public float playerStrength = 1f;
    //Player ability status
    public int abilityStatus;
    //Amount of enemies the player has killed
    public int enemiesKilled = 0;
    //The character type, will be chosen in the character creator
    public string characterType;
    //The prefab of the player will also be chosen in the character creator
    public string characterPrefab;
    //The amount of pinapples the player has 
    public int numPinapple = 0;
    //The amount of peaches the player has
    public int numPeach = 0;
    //The amount of Strawberrys the player has 
    public int numStrawberry = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
        //Get the playerCombat script that is connected to the game object the script is attached to.
        playerCombatScript = GetComponent<PlayerCombat>();
        //Get the playerCombat script that is connected to the game object the script is attached to.
        inventoryManagerScript = GetComponent<InventoryManager>();
        //Set the animator variable to the playerMovement scripts animator.
        animator = playerMovementScript.animator;
        playerHealth *= playerLevel;
    }

    // Update is called with the physics system 50 times per second
    void FixedUpdate()
    {
        //Get the amount of food the player has picked up
        numPinapple = inventoryManagerScript.numPinapple;
        numPeach = inventoryManagerScript.numPeach;
        numStrawberry = inventoryManagerScript.numStrawberry;
        //Set the animator Health parameter to the players current health every fram
        animator.SetFloat("Health", playerHealth);
        //Check if the player health drops below 0
        if (playerHealth <= 0)
        {
            //Start the death animation
            animator.SetTrigger("Death");
            //If the players chosen type is swordsman and their ability is at 11- playerlevel, so as the player
            //levels up, the can use their ability more often
            if (characterType == "swordsman" && playerCombatScript.characterAbilityStatus == (11 - playerLevel))
            {
                //revive them
                playerCombatScript.SMAbility();
            }
            else
            {
                //Otherwise, kill them
                PlayerDeath();
            }
        }
        //If the player xp equals 10 level them up
        if (playerXP == 5)
        {
            playerLevel++;
            //Reset the player XP
            playerXP = 0;
        }
    }

    //Called when player takes damage
    public void PlayerTakeDamage(float damage)
    {
        //set hurt animator paramater take away HP
        animator.SetTrigger("Hurt");
        playerHealth -= damage;
    }

    //Called when a player dies
    public void PlayerDeath()
    {
        //Destroy the playerMovementScript so that the player is unable to move.
        Destroy(playerMovementScript);
    }
    //Save the progress of the player
    public void SaveProgress()
    {
        //Get the ability status
        abilityStatus = playerCombatScript.characterAbilityStatus;
        //Call the save player data method, 'this' refers to this script
        SaveSystem.SavePlayerData(this);
    }
    //Load the progress of the player
    public void LoadProgress()
    {
        //Call the load player method and store the return value
        PlayerData savedPlayerData = SaveSystem.LoadPlayer();
        characterType = savedPlayerData.characterType;
        characterPrefab = savedPlayerData.characterPrefab;
        playerLevel = savedPlayerData.playerLevel;
        playerStrength = savedPlayerData.playerStrength;
        //Make the ability status from the save the combat scripts ability status
        playerCombatScript.characterAbilityStatus = savedPlayerData.characterAbilityStatus;
        enemiesKilled = savedPlayerData.enemyKillCount;
        //Make the player position the stored position
        transform.position = new Vector3(savedPlayerData.playerPosition[0], savedPlayerData.playerPosition[1], savedPlayerData.playerPosition[2]);
        //Store the amount of collected items in the inventory manager script.
        inventoryManagerScript.numPinapple = savedPlayerData.inventoryItemAmounts[0];
        inventoryManagerScript.numPeach = savedPlayerData.inventoryItemAmounts[1];
        inventoryManagerScript.numStrawberry = savedPlayerData.inventoryItemAmounts[2];
    }

}
