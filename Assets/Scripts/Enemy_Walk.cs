using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walk : StateMachineBehaviour
{
    //The speed the boss travels at
    private readonly float speed = 1f;
    //The target to follow
    private Transform target;
    //The rigidbody of the boss
    private Rigidbody2D enemyRB;
    //The ai script of the boss
    private EnemyAI enemyAIScript;
    //The attack range of the boss
    private readonly float attackRange = 0.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get the target, boss rigidbody and the boss ai script
        target = GameObject.FindWithTag("Player").transform;
        enemyRB = animator.GetComponent<Rigidbody2D>();
        enemyAIScript = animator.GetComponent<EnemyAI>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //The target position to move to
        Vector2 targetPos = new Vector2(target.position.x, enemyRB.position.y);
        //The new pos of the boss
        Vector2 newpos = Vector2.MoveTowards(enemyRB.position, targetPos, speed * Time.fixedDeltaTime);
        //Move to the new pos
        enemyRB.MovePosition(newpos);
        //check to see if the boss sprite needs to be flipped
        enemyAIScript.EnemyFlip();
        //If the player is in attack range
        if (Vector2.Distance(targetPos, enemyRB.position) < attackRange)
        {
            //Play the attack animation
            animator.SetTrigger("Attack");
        }
    }
}
