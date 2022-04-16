using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerMovement playerMovementScript;
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerLife playerLifeScript;
    //Animator component of the sprite
    private Animator animator;
    //The point in which a circle detecting enemies will be created
    public Transform AttackArea;
    //The layer in which enemies will be searched for
    public LayerMask enemyLayer;
    //The range of the attack
    public float attackRange = 0.5f;
    //Character damage amount
    public float playerAttackDamage = 30f;
    //Characters ability process
    public int characterAbilityStatus = 0;
    //The character type, will be chosen in the character creator
    public static string characterType;

    

    // Start is called before the first frame update
    void Start()
    {
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
        //Get the playerLife script that is connected to the game object the script is attached to.
        playerLifeScript = GetComponent<PlayerLife>();
        //Set the animator variable to the playerMovement scripts animator.
        animator = playerMovementScript.animator;
        characterAbilityStatus = 10;
        characterType = "sScout";
    }

    // Update is called once per frame
    void Update()
    {
        //If the player presses q and they are not currently jumping
        if (Input.GetButtonDown("Attack") && !animator.GetBool("Jump"))
        {
            Attack();

        }
        //Ability's
        if (Input.GetButtonDown("Ability") && characterAbilityStatus == 10)
        {
            if (characterType == "swordsman")
            {
                //Swordsman Ability
            }
            if (characterType == "sScout")
            {
                SSAbility();
                //Skeleton scout ability
            }
            if (characterType == "wizard")
            {
                //wizard ability
            }

        }
    }
    //Called when 'q' is pressed
    void Attack()
    {
        //Start the attack animation
        animator.SetTrigger("Attack");
        GetAndKillEnemies();
    }
    //Skeleton Scout Ability
    void SSAbility()
    {
        //Get the rigidbody component of the player
        Rigidbody2D charRB = GetComponent<Rigidbody2D>();
        //Start the ability animation, this animation has an animation event every frame which calls the GetAndKillEnemies() method
        animator.SetTrigger("Ability");
        //Add a force to the players rigidbody
        charRB.AddForce(transform.right * 2500);
        //reset the ability status of the player
        characterAbilityStatus = 0;
    }
    //Find all the enemies in range of the attack and kill them
    void GetAndKillEnemies()
    {
        //Get all the enemies in attackRange of attackArea
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackArea.position, attackRange, enemyLayer);
        //For each enemy that was in range of the attack when it occured 
        foreach (Collider2D enemy in hitEnemies)
        {
            //If the enemy is a boss
            if (enemy.CompareTag("Boss"))
            {
                //Take health from the boss
                //enemy.GetComponent<Boss>().TakeHealth();
            }
            else
            {
                //Run the enemy death script
                enemy.GetComponent<Enemy>().EnemyDeath();
                //Destroy the box collider so the player is able to run over the body.
                Destroy(enemy.GetComponent<BoxCollider2D>());
                //Increment the character ability status
                characterAbilityStatus++;
                //Increment playerXP, amount of enemies killed and increase the player's strength by 0.2
                playerLifeScript.playerXP++;
                playerLifeScript.enemiesKilled++;
                playerLifeScript.playerStrength += 0.2f;
                //Multiply the attack damage by the players strength
                playerAttackDamage *= playerLifeScript.playerStrength;
            }

        }
    }

    //Debug
    /*
    private void OnDrawGizmosSelected()
    {
        if (AttackArea == null)
            return;

        Gizmos.DrawWireSphere(AttackArea.position, attackRange);
    }
    */
}
