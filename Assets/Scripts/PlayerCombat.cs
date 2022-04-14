using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Animator component of the sprite
    public Animator animator;
    //The point in which a circle detecting enemies will be created
    public Transform AttackArea;
    //The layer in which enemies will be searched for
    public LayerMask enemyLayer;
    //The range of the attack
    public float attackRange = 0.5f;
    //Characters ability process
    public int characterAbilityStatus = 0;
    //The character type, will be chosen in the character creator
    public static string characterType;
    //Reference to PlayerMovement Script to access methods contained inside
    private PlayerMovement playerMovementScript;


    // Start is called before the first frame update
    void Start()
    {
        //Get the playerMovement script that is connected to the game object the script is attached to.
        playerMovementScript = GetComponent<PlayerMovement>();
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
                //Swordsman ability
            }
            if(characterType == "sScout")
            {
                //Skeleton scout ability
            }
            if(characterType == "wizard")
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
        //Get all the enemies in attackRange of attackArea
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackArea.position, attackRange, enemyLayer);
        //For each enemy that was in range of the attack when it occured 
        foreach (Collider2D enemy in hitEnemies)
        {
            //Run the enemy death script
            enemy.GetComponent<Enemy>().EnemyDeath();
            //Destroy the box collider so the player is able to run over the body.
            Destroy(enemy.GetComponent<BoxCollider2D>());
            //Increment the character ability status
            characterAbilityStatus++;
        }
    }

    void TakeDamage()
    {
        //set damaged animator paramater take away HP
    }

    //Debug
    private void OnDrawGizmosSelected()
    {
        if (AttackArea == null)
            return;

        Gizmos.DrawWireSphere(AttackArea.position, attackRange);
    }
}
