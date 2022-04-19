using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyAI : MonoBehaviour
{
    //Enemy animator
    public Animator enemyAnimator;
    //the position of the gameObject the enemy is targeting
    private Transform target;
    //The range of the attack
    public float attackRange = 1f;
    //Enemy attack damage
    public int attackDamage = 15;
    //The layer in which enemies will be searched for
    public LayerMask playerLayer;
    //The point in which a circle detecting enemies will be created
    public Transform attackArea;
    //facing left
    public bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        //Get the target transform
        target = GameObject.FindWithTag("Player").transform;
    }

    //Called every frame
    private void Update()
    {
        //If the distance between the play and the boss is less than 5 
        if (Mathf.Abs(Vector2.Distance(transform.position, target.position)) <= 3)
        {
            //Start the moving animation
            enemyAnimator.SetTrigger("EnemyTargeted");
        }
    }

    public void EnemyFlip()
    {
        //if the boss' position is less than the targets position and it is facing left 
        if (transform.position.x < target.position.x && facingLeft)
        {
            //Flip the direction it is facing
            facingLeft = !facingLeft;
            transform.Rotate(0f, 180f, 0f);
        }
        //If the boss' position is greater than the targets position and its not facing right 
        else if (transform.position.x > target.position.x && !facingLeft)
        {
            //Flip direction it is facing
            facingLeft = !facingLeft;
            transform.Rotate(0f, 180f, 0f);

        }
    }
    //Called when an enemy dies. 
    public void EnemyDeath()
    {
        //Set animation
        enemyAnimator.SetTrigger("Dead");
        //Destroy the box collider so the player can walk over the dead enemy
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(this);
    }

    public void EnemyAttackPlayer()
    {
        Collider2D playerGameObject = Physics2D.OverlapCircle(attackArea.position, attackRange, playerLayer);
        //take attackDamage from the players health
        if (playerGameObject != null)
        {
            playerGameObject.GetComponent<PlayerLife>().PlayerTakeDamage(attackDamage);
        }
    }
}
