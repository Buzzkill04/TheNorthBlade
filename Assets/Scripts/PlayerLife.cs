using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerMovement playerMovementScript;
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerCombat playerCombatScript;
    //Reference to PlayerLife Script to access methods contained inside
    private PlayerLife playerLifeScript;
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
    //Amount of enemies the player has killed
    public int enemiesKilled = 0;
    //The character type, will be chosen in the character creator
    public string characterType;

    // Start is called before the first frame update
    void Start()
    {
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
        //Get the playerCombat script that is connected to the game object the script is attached to.
        playerCombatScript = GetComponent<PlayerCombat>();
        //Get the playerCombat script that is connected to the game object the script is attached to.
        playerLifeScript = GetComponent<PlayerLife>();
        //Set the animator variable to the playerMovement scripts animator.
        animator = playerMovementScript.animator;
        playerHealth *= playerLevel;
    }

    // Update is called with the physics system 50 times per second
    void FixedUpdate()
    {
        //Set the animator Health parameter to the players current health every fram
        animator.SetFloat("Health", playerHealth);
        //Check if the player health drops below 0
        if (playerHealth <= 0)
        {
            //Start the death animation
            animator.SetTrigger("Death");
            //If the players chosen type is swordsman and their ability is at 10
            if (characterType == "swordsman" && playerCombatScript.characterAbilityStatus == 10)
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
        if (playerXP == 10)
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
    
}
