using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    //The boss' health
    public float bossHealth = 200f;
    //Animator component of the boss
    public Animator bossAnimator;
    //Player's layer so that the boss only needs to look for game objects at that layer
    public LayerMask playerLayer;
    //The amount of damage the boss does
    public int attackDamage = 30;
    //The amount of damage the boss does in the super attack
    public int superAttackDamage = 40;
    //The range of the attack
    public float attackRange = 1f;
    //The point in which a circle detecting enemies will be created
    public Transform attackArea;
    //Target
    private Transform target;
    //If the boss is facing left
    public bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        //Get the target's position
        target = GameObject.FindWithTag("Player").transform;
    }

    //Called every frame
    private void Update()
    {
        //If the distance between the play and the boss is less than 5 
        if (Mathf.Abs(Vector2.Distance(transform.position, target.position)) <= 3)
        {
            //Start the targeting/moving trigger
            bossAnimator.SetTrigger("EnemyTargeted");
        }
        
    }

    public void BossFlip()
    {
        //if the boss' position is less than the targets position and it is facing left 
        if (transform.position.x < target.position.x && facingLeft)
        {
            //Flip the direction it is facing
            facingLeft = !facingLeft;
            transform.Rotate(0f, 180f, 0f);
        }//If the boss' position is greater than the targets position and its not facing right 
        else if (transform.position.x > target.position.x && !facingLeft)
        {
            //Flip direction it is facing
            facingLeft = !facingLeft;
            transform.Rotate(0f, 180f, 0f);
            
        }
    }
    //Take damage
    public void BossTakeDamage(float damage)
    {
        //Take damage
        bossHealth -= damage;
        //If the boss' health is less than or equal to 0 
        if (bossHealth <= 0)
        {
            //Start the death method
            BossDeath();
        }
    }

    public void BossDeath()
    {
        //Play the death animation
        bossAnimator.SetBool("Dead", true);
        //Destroy the Rigidbody
        Destroy(GetComponent<Rigidbody2D>());
        //Destroy the collider
        Destroy(GetComponent<BoxCollider2D>());
        //Get the northblade  prefab
        GameObject NorthBladePrefab = (GameObject)Resources.Load("Prefabs/TheNorthBlade");
        //Create a new north blade prefab at the boss' position when they died
        Instantiate(NorthBladePrefab, transform.position, transform.rotation);
    }

    //Find all the enemies in range of the attack and kill them
    public void AttackPlayer()
    {
        //Overlap circle returns 1 element
        Collider2D playerGameObject = Physics2D.OverlapCircle(attackArea.position, attackRange, playerLayer);
        //take attackDamage from the players health
        if (playerGameObject != null)
        {
            PlayerLife playerLifeScript;
            playerLifeScript = playerGameObject.GetComponent<PlayerLife>();
            if (playerLifeScript.playerHealth > 0)
            {
                playerLifeScript.PlayerTakeDamage(attackDamage);
            }
        }
        
    }

    public void SuperAttackPlayer()
    {
        //Overlap circle returns 1 element
        Collider2D playerGameObject = Physics2D.OverlapCircle(attackArea.position, attackRange, playerLayer);
        //take attackDamage from the players health
        if (playerGameObject != null)
        {
            PlayerLife playerLifeScript;
            playerLifeScript = playerGameObject.GetComponent<PlayerLife>();
            if (playerLifeScript.playerHealth > 0)
            {
                playerLifeScript.PlayerTakeDamage(superAttackDamage);
            }
        }
    }
    

}

