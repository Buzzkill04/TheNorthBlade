using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerMovement playerMovementScript;
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

    // Start is called before the first frame update
    void Start()
    {
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
        //Set the animator variable to the playerMovement scripts animator.
        animator = playerMovementScript.animator;
        playerHealth *= playerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the animator Health parameter to the players current health every fram
        animator.SetFloat("Health", playerHealth);
        //Check if the player health drops below 0
        if (playerHealth <= 0)
        {
            PlayerDeath();
        }
        if (playerXP == 10)
        {
            playerLevel++;
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
        //Start the death animation
        animator.SetTrigger("Death");
        //Destroy the playerMovementScript so that the player is unable to move.
        Destroy(playerMovementScript);
    }
    
}
