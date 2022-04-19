using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    //The speed the boss travels at
    private readonly float speed = 1f;
    //The target to follow
    private Transform target;
    //The rigidbody of the boss
    private Rigidbody2D bossRB;
    //The ai script of the boss
    private BossAI bossAIScript;
    //The attack range of the boss
    private readonly float attackRange = 0.3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get the target, boss rigidbody and the boss ai script
        target = GameObject.FindWithTag("Player").transform;
        bossRB = animator.GetComponent<Rigidbody2D>();
        bossAIScript = animator.GetComponent<BossAI>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //The target position to move to
        Vector2 targetPos = new Vector2(target.position.x, bossRB.position.y);
        //The new pos of the boss
        Vector2 newpos = Vector2.MoveTowards(bossRB.position, targetPos, speed * Time.fixedDeltaTime);
        //Move to the new pos
        bossRB.MovePosition(newpos);
        //check to see if the boss sprite needs to be flipped
        bossAIScript.BossFlip();
        //If the player is in attack range
        if (Vector2.Distance(targetPos, bossRB.position) < attackRange)
        {
            //choose a random attack to play
            switch ((int)Random.Range(1, 3))
            {
                case 1:
                    animator.SetTrigger("Attack");
                    break;
                case 2:
                    animator.SetTrigger("GAttack");
                    break;
                default:
                    break;
            }
        }
    }
}
