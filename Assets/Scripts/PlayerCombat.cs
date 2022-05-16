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
    public float playerAttackDamage;
    //Characters ability process
    public int characterAbilityStatus = 0;
    //Character type
    private string characterType;
    //The attack button to listen to
    public string attackButton = "Attack";
    //If the player is the second player in a multiplayer game
    public bool isSecondPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
        //Get the playerLife script that is connected to the game object the script is attached to.
        playerLifeScript = GetComponent<PlayerLife>();
        //Set the animator variable to the playerMovement scripts animator.
        animator = playerMovementScript.animator;
        //Get the chosen characterType
        characterType = playerLifeScript.characterType;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player presses q and they are not currently jumping
        if (Input.GetButtonDown(attackButton) && !animator.GetBool("Jump"))
        {
            Attack();
        }
        //Ability's
        //the players level is taken away from 7 so that the more they level up they can use the ability more often
        if (Input.GetButtonDown("Ability") && characterAbilityStatus >= (7 - playerLifeScript.playerLevel))
        {
            if (characterType == "sScout")
            {
                //Skeleton scout ability
                SSAbility();
            }
            if (characterType == "wizard")
            {
                //wizard ability
                WIZAbility();
            }
        }
    }
    //Called when 'q' is pressed
    void Attack()
    {
        //Start the attack animation
        animator.SetTrigger("Attack");
        //get all the enemies in range and kill them
        GetAndKillEnemies();
        //Play the attack audio
        FindObjectOfType<AudioManager>().playSound("attack");
    }
    //Skeleton Scout Ability
    public void SSAbility()
    {
        //Get the rigidbody component of the player
        Rigidbody2D charRB = GetComponent<Rigidbody2D>();
        //Start the ability animation, this animation has an animation event every frame which calls the GetAndKillEnemies() method
        animator.SetTrigger("Ability");
        //Add a force to the players rigidbody
        charRB.AddForce(transform.right * 2500);
        //reset the ability status of the player
        characterAbilityStatus = 0;
        FindObjectOfType<AudioManager>().playSound("attack");
    }
    //Swordsman ability
    public void SMAbility()
    {
        //Start the recover animation
        animator.SetTrigger("Ability");    
        //Set the player health to 50
        playerLifeScript.playerHealth = 50;
        //Reset the player's ability status
        characterAbilityStatus = 0;
        FindObjectOfType<AudioManager>().playSound("attack");
    }
    //Wizard ability
    public void WIZAbility()
    {
        //Start the ability animation
        animator.SetTrigger("Ability");
        //Get the fireball prefab
        GameObject fireballPrefab = (GameObject)Resources.Load("Prefabs/Fireball");
        //Create a new fireball prefab at the attack position
        Instantiate(fireballPrefab, AttackArea.position, AttackArea.rotation);
        //Reset the ability status
        characterAbilityStatus = 0;
        FindObjectOfType<AudioManager>().playSound("attack");
    }


    //Find all the enemies in range of the attack and kill them
    void GetAndKillEnemies()
    {
        //Get all the enemies in attackRange of attackArea
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackArea.position, attackRange, enemyLayer);
        if (MainMenu.isMultiplayer)
        {
            if (isSecondPlayer)
            {
                //Get the layer mask of the player
                LayerMask playerLayer = LayerMask.GetMask("Player");
                //Get the hit player
                Collider2D hitPlayer1 = Physics2D.OverlapCircle(AttackArea.position, attackRange, playerLayer);
                //If the hit player is not equal to null
                if (hitPlayer1 != null)
                {
                    //damage the player
                    hitPlayer1.GetComponent<PlayerLife>().PlayerTakeDamage(playerAttackDamage);
                }
                return;
            }
            //If the player attacking is not the second player
            Collider2D hitPlayer2 = Physics2D.OverlapCircle(AttackArea.position, attackRange, enemyLayer);
            //If it is not equal to null
            if (hitPlayer2 != null)
            {
                //damage the player
                hitPlayer2.GetComponent<PlayerLife>().PlayerTakeDamage(playerAttackDamage);
            }
            return;

        }
        //For each enemy that was in range of the attack when it occured 
        foreach (Collider2D enemy in hitEnemies)
        {
            //If the enemy is a boss
            if (enemy.CompareTag("Boss"))
            {
                //Take health from the boss
                enemy.GetComponent<BossAI>().BossTakeDamage(playerAttackDamage);
                characterAbilityStatus++;
            }
            else
            {
                //Run the enemy death script
                enemy.GetComponent<EnemyAI>().EnemyDeath();
                //Destroy the box collider so the player is able to run over the body.
                Destroy(enemy.GetComponent<BoxCollider2D>());
                //Increment the character ability status
                characterAbilityStatus++;
                //Increment playerXP, amount of enemies killed and increase the player's strength by 0.2
                playerLifeScript.playerXP++;
                playerLifeScript.enemiesKilled++;
                playerLifeScript.playerStrength += 0.2f;
                //Multiply the attack damage by the players strength
                playerAttackDamage = 30 * playerLifeScript.playerStrength;
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
